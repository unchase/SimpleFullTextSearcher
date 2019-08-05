using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleFullTextSearcher.FileSearcher.Helpers
{
    public static class TextFileEncodingHelper
    {
        private const long DefaultHeuristicSampleSize = 0x10000; //completely arbitrary - inappropriate for high numbers of files / high speed requirements

        public static Encoding DetectTextFileEncoding(string inputFilename)
        {
            using (var textfileStream = File.OpenRead(inputFilename))
            {
                return DetectTextFileEncoding(textfileStream);
            }
        }

        public static Encoding DetectTextFileEncoding(FileStream inputFileStream)
        {
            if (inputFileStream == null)
                throw new ArgumentNullException(nameof(inputFileStream), T._("Should be valid FileStream!")); //@"Необходимо представить валидный Filestream!"));

            if (!inputFileStream.CanRead)
                throw new ArgumentException(T._("The FileStream does not support read operation!")); //@"Представленный FileStream не поддерживает операцию чтения!", nameof(inputFileStream));

            if (!inputFileStream.CanSeek)
                throw new ArgumentException(T._("The FileStream does not support search operation!")); //@"Представленный FileStream не поддерживает операцию поиска!", nameof(inputFileStream));

            var originalPos = inputFileStream.Position;

            inputFileStream.Position = 0;

            //First read only what we need for BOM detection
            var bomBytes = new byte[inputFileStream.Length > 4 ? 4 : inputFileStream.Length];
            inputFileStream.Read(bomBytes, 0, bomBytes.Length);

            var encodingFound = DetectBomBytes(bomBytes);

            if (encodingFound != null)
            {
                inputFileStream.Position = originalPos;
                return encodingFound;
            }

            //BOM Detection failed, going for heuristics now.
            //  create sample byte array and populate it
            var sampleBytes = new byte[DefaultHeuristicSampleSize > inputFileStream.Length ? inputFileStream.Length : DefaultHeuristicSampleSize];
            Array.Copy(bomBytes, sampleBytes, bomBytes.Length);
            if (inputFileStream.Length > bomBytes.Length)
                inputFileStream.Read(sampleBytes, bomBytes.Length, sampleBytes.Length - bomBytes.Length);
            inputFileStream.Position = originalPos;

            //test byte array content
            encodingFound = DetectUnicodeInByteSampleByHeuristics(sampleBytes);

            return encodingFound;
        }

        public static Encoding DetectBomBytes(byte[] bomBytes)
        {
            if (bomBytes == null)
                throw new ArgumentNullException(nameof(bomBytes), T._("Should be valid BOM bytes array!")); //@"Необходимо представить валидный BOM массив байтов!"));

            if (bomBytes.Length < 2)
                return null;

            if (bomBytes[0] == 0xff && bomBytes[1] == 0xfe &&
                (bomBytes.Length < 4 || bomBytes[2] != 0 || bomBytes[3] != 0))
                return Encoding.Unicode;

            if (bomBytes[0] == 0xfe && bomBytes[1] == 0xff)
                return Encoding.BigEndianUnicode;

            if (bomBytes.Length < 3)
                return null;

            if (bomBytes[0] == 0xef && bomBytes[1] == 0xbb && bomBytes[2] == 0xbf)
                return Encoding.UTF8;

            if (bomBytes[0] == 0x2b && bomBytes[1] == 0x2f && bomBytes[2] == 0x76)
                return Encoding.UTF7;

            if (bomBytes.Length < 4)
                return null;

            if (bomBytes[0] == 0xff && bomBytes[1] == 0xfe && bomBytes[2] == 0 && bomBytes[3] == 0)
                return Encoding.UTF32;

            if (bomBytes[0] == 0 && bomBytes[1] == 0 && bomBytes[2] == 0xfe && bomBytes[3] == 0xff)
                return Encoding.GetEncoding(12001);

            return null;
        }

        public static Encoding DetectUnicodeInByteSampleByHeuristics(byte[] sampleBytes)
        {
            long oddBinaryNullsInSample = 0;
            long evenBinaryNullsInSample = 0;
            long suspiciousUtf8SequenceCount = 0;
            long suspiciousUtf8BytesTotal = 0;
            long likelyUsasciiBytesInSample = 0;

            //Cycle through, keeping count of binary null positions, possible UTF-8
            //  sequences from upper ranges of Windows-1252, and probable US-ASCII
            //  character counts.
            long currentPos = 0;
            var skipUtf8Bytes = 0;

            while (currentPos < sampleBytes.Length)
            {
                //binary null distribution
                if (sampleBytes[currentPos] == 0)
                {
                    if (currentPos % 2 == 0)
                        evenBinaryNullsInSample++;
                    else
                        oddBinaryNullsInSample++;
                }

                //likely US-ASCII characters
                if (IsCommonUsAsciiByte(sampleBytes[currentPos]))
                    likelyUsasciiBytesInSample++;

                //suspicious sequences (look like UTF-8)
                if (skipUtf8Bytes == 0)
                {
                    var lengthFound = DetectSuspiciousUtf8SequenceLength(sampleBytes, currentPos);

                    if (lengthFound > 0)
                    {
                        suspiciousUtf8SequenceCount++;
                        suspiciousUtf8BytesTotal += lengthFound;
                        skipUtf8Bytes = lengthFound - 1;
                    }
                }
                else
                {
                    skipUtf8Bytes--;
                }

                currentPos++;
            }

            //1: UTF-16 LE - in english / european environments, this is usually characterized by a
            //  high proportion of odd binary nulls (starting at 0), with (as this is text) a low
            //  proportion of even binary nulls.
            //  The thresholds here used (less than 20% nulls where you expect non-nulls, and more than
            //  60% nulls where you do expect nulls) are completely arbitrary.

            if (evenBinaryNullsInSample * 2.0 / sampleBytes.Length < 0.2 &&
                oddBinaryNullsInSample * 2.0 / sampleBytes.Length > 0.6)
                return Encoding.Unicode;


            //2: UTF-16 BE - in english / european environments, this is usually characterized by a
            //  high proportion of even binary nulls (starting at 0), with (as this is text) a low
            //  proportion of odd binary nulls.
            //  The thresholds here used (less than 20% nulls where you expect non-nulls, and more than
            //  60% nulls where you do expect nulls) are completely arbitrary.

            if (oddBinaryNullsInSample * 2.0 / sampleBytes.Length < 0.2 &&
                evenBinaryNullsInSample * 2.0 / sampleBytes.Length > 0.6)
                return Encoding.BigEndianUnicode;


            //3: UTF-8 - Martin Dürst outlines a method for detecting whether something CAN be UTF-8 content
            //  using regexp, in his w3c.org unicode FAQ entry:
            //  http://www.w3.org/International/questions/qa-forms-utf-8
            //  adapted here for C#.
            var potentiallyMangledString = Encoding.ASCII.GetString(sampleBytes);
            var utf8Validator = new Regex(@"\A("
                + @"[\x09\x0A\x0D\x20-\x7E]"
                + @"|[\xC2-\xDF][\x80-\xBF]"
                + @"|\xE0[\xA0-\xBF][\x80-\xBF]"
                + @"|[\xE1-\xEC\xEE\xEF][\x80-\xBF]{2}"
                + @"|\xED[\x80-\x9F][\x80-\xBF]"
                + @"|\xF0[\x90-\xBF][\x80-\xBF]{2}"
                + @"|[\xF1-\xF3][\x80-\xBF]{3}"
                + @"|\xF4[\x80-\x8F][\x80-\xBF]{2}"
                + @")*\z");
            if (!utf8Validator.IsMatch(potentiallyMangledString)) return null;
            //Unfortunately, just the fact that it CAN be UTF-8 doesn't tell you much about probabilities.
            //If all the characters are in the 0-127 range, no harm done, most western charsets are same as UTF-8 in these ranges.
            //If some of the characters were in the upper range (western accented characters), however, they would likely be mangled to 2-byte by the UTF-8 encoding process.
            // So, we need to play stats.

            // The "Random" likelihood of any pair of randomly generated characters being one
            //   of these "suspicious" character sequences is:
            //     128 / (256 * 256) = 0.2%.
            //
            // In western text data, that is SIGNIFICANTLY reduced - most text data stays in the <127
            //   character range, so we assume that more than 1 in 500,000 of these character
            //   sequences indicates UTF-8. The number 500,000 is completely arbitrary - so sue me.
            //
            // We can only assume these character sequences will be rare if we ALSO assume that this
            //   IS in fact western text - in which case the bulk of the UTF-8 encoded data (that is
            //   not already suspicious sequences) should be plain US-ASCII bytes. This, I
            //   arbitrarily decided, should be 80% (a random distribution, eg binary data, would yield
            //   approx 40%, so the chances of hitting this threshold by accident in random data are
            //   VERY low).

            if (suspiciousUtf8SequenceCount * 500000.0 / sampleBytes.Length >= 1 //suspicious sequences
                && (
                    //all suspicious, so cannot evaluate proportion of US-Ascii
                    sampleBytes.Length - suspiciousUtf8BytesTotal == 0 || likelyUsasciiBytesInSample * 1.0 /
                    (sampleBytes.Length - suspiciousUtf8BytesTotal) >= 0.8))
                return Encoding.UTF8;

            return null;
        }

        private static bool IsCommonUsAsciiByte(byte testByte) => (
            testByte == 0x0A //lf
            || testByte == 0x0D //cr
            || testByte == 0x09 //tab
            || testByte >= 0x20 && testByte <= 0x2F //common punctuation
            || testByte >= 0x30 && testByte <= 0x39 //digits
            || testByte >= 0x3A && testByte <= 0x40 //common punctuation
            || testByte >= 0x41 && testByte <= 0x5A //capital letters
            || testByte >= 0x5B && testByte <= 0x60 //common punctuation
            || testByte >= 0x61 && testByte <= 0x7A //lowercase letters
            || testByte >= 0x7B && testByte <= 0x7E
        );

        private static int DetectSuspiciousUtf8SequenceLength(byte[] sampleBytes, long currentPos)
        {
            var lengthFound = 0;

            if (sampleBytes.Length >= currentPos + 1 && sampleBytes[currentPos] == 0xC2)
            {
                if (sampleBytes[currentPos + 1] == 0x81 || sampleBytes[currentPos + 1] == 0x8D ||
                    sampleBytes[currentPos + 1] == 0x8F)
                    lengthFound = 2;
                else if (sampleBytes[currentPos + 1] == 0x90 || sampleBytes[currentPos + 1] == 0x9D)
                    lengthFound = 2;
                else if (sampleBytes[currentPos + 1] >= 0xA0 && sampleBytes[currentPos + 1] <= 0xBF)
                    lengthFound = 2;
            }
            else if (sampleBytes.Length >= currentPos + 1 && sampleBytes[currentPos] == 0xC3)
            {
                if (sampleBytes[currentPos + 1] >= 0x80 && sampleBytes[currentPos + 1] <= 0xBF)
                    lengthFound = 2;
            }
            else if (sampleBytes.Length >= currentPos + 1 && sampleBytes[currentPos] == 0xC5)
            {
                if (sampleBytes[currentPos + 1] == 0x92 || sampleBytes[currentPos + 1] == 0x93)
                    lengthFound = 2;
                else if (sampleBytes[currentPos + 1] == 0xA0 || sampleBytes[currentPos + 1] == 0xA1)
                    lengthFound = 2;
                else if (sampleBytes[currentPos + 1] == 0xB8 || sampleBytes[currentPos + 1] == 0xBD ||
                         sampleBytes[currentPos + 1] == 0xBE)
                    lengthFound = 2;
            }
            else if (sampleBytes.Length >= currentPos + 1 && sampleBytes[currentPos] == 0xC6)
            {
                if (sampleBytes[currentPos + 1] == 0x92)
                    lengthFound = 2;
            }
            else if (sampleBytes.Length >= currentPos + 1 && sampleBytes[currentPos] == 0xCB)
            {
                if (sampleBytes[currentPos + 1] == 0x86 || sampleBytes[currentPos + 1] == 0x9C)
                    lengthFound = 2;
            }
            else if (sampleBytes.Length >= currentPos + 2 && sampleBytes[currentPos] == 0xE2)
            {
                if (sampleBytes[currentPos + 1] == 0x80)
                {
                    if (sampleBytes[currentPos + 2] == 0x93 || sampleBytes[currentPos + 2] == 0x94)
                        lengthFound = 3;
                    if (sampleBytes[currentPos + 2] == 0x98 || sampleBytes[currentPos + 2] == 0x99 ||
                        sampleBytes[currentPos + 2] == 0x9A)
                        lengthFound = 3;
                    if (sampleBytes[currentPos + 2] == 0x9C || sampleBytes[currentPos + 2] == 0x9D ||
                        sampleBytes[currentPos + 2] == 0x9E)
                        lengthFound = 3;
                    if (sampleBytes[currentPos + 2] == 0xA0 || sampleBytes[currentPos + 2] == 0xA1 ||
                        sampleBytes[currentPos + 2] == 0xA2)
                        lengthFound = 3;
                    if (sampleBytes[currentPos + 2] == 0xA6)
                        lengthFound = 3;
                    if (sampleBytes[currentPos + 2] == 0xB0)
                        lengthFound = 3;
                    if (sampleBytes[currentPos + 2] == 0xB9 || sampleBytes[currentPos + 2] == 0xBA)
                        lengthFound = 3;
                }
                else if (sampleBytes[currentPos + 1] == 0x82 && sampleBytes[currentPos + 2] == 0xAC)
                    lengthFound = 3;
                else if (sampleBytes[currentPos + 1] == 0x84 && sampleBytes[currentPos + 2] == 0xA2)
                    lengthFound = 3;
            }

            return lengthFound;
        }
    }
}
