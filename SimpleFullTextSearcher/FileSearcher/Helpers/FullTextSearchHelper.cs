using System;
using System.IO;
using System.Text;
using Aspose.Cells;
using Code7248.word_reader;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace SimpleFullTextSearcher.FileSearcher.Helpers
{
    public static class FullTextSearchHelper
    {
        public static bool FindTextInPdf(string fileFullPath, string text) => (GetTextFromPdf(fileFullPath).IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);

        private static string GetTextFromPdf(string fileFullPath)
        {
            var text = new StringBuilder();
            try
            {
                using (var reader = new PdfReader(fileFullPath))
                {
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                }
            }
            catch (Exception)
            {
                return text.ToString();
            }

            return text.ToString();
        }

        public static bool FindTextInWord(string fileFullPath, string text)
        {
            try
            {
                var extractor = new TextExtractor(fileFullPath);
                return extractor.ExtractText().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static bool FindTextInExcell(string fileFullPath, string text)
        {
            try
            {
                LoadOptions loadOptions = null;
                switch (new FileInfo(fileFullPath).Extension.ToLower())
                {
                    case ".xls":
                        loadOptions = new LoadOptions(LoadFormat.Excel97To2003);
                        break;
                    case ".xlsx":
                        loadOptions = new LoadOptions(LoadFormat.Xlsx);
                        break;
                }
                
                var wbExcel2007 = new Workbook(fileFullPath, loadOptions);
                foreach (var sheet in wbExcel2007.Worksheets)
                {
                    foreach (Cell cell in sheet.Cells)
                    {
                        if (cell.StringValue.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                            return true;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
