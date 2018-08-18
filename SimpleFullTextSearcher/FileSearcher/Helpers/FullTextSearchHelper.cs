using System;
using System.IO;
using System.Text;
using Aspose.Cells;
using Code7248.word_reader;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spire.Doc;
using Spire.Pdf;
using PdfDocument = Spire.Pdf.PdfDocument;

namespace SimpleFullTextSearcher.FileSearcher.Helpers
{
    public static class FullTextSearchHelper
    {
        // Spire.Office может обрабатывать только 10 файлов, поэтому не задействуем его. Если приобрести лицензию, то можно раскомментировать
        //#region Spire.Office

        //public static bool FindTextInPdf(string fileFullPath, string text)
        //{
        //    try
        //    {
        //        var pdf = new PdfDocument(fileFullPath);
        //        foreach (PdfPageBase page in pdf.Pages)
        //        {
        //            if (page.FindText(text).Finds.Any() || page.FindText(text.ToLower()).Finds.Any())
        //                return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public static bool FindTextInWordFile(string fileFullPath, string text)
        //{
        //    try
        //    {
        //        var doc = new Document(fileFullPath);
        //        return doc.GetText().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public static bool FindTextInExcellFile(string fileFullPath, string text)
        //{
        //    try
        //    {
        //        var workbook = new Workbook();
        //        workbook.LoadFromFile(fileFullPath);
        //        foreach (var sheet in workbook.Worksheets)
        //        {
        //            foreach (var range in sheet.Range)
        //            {
        //                if (range.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
        //                    return true;
        //            }
        //        }

        //        return false;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //#endregion

        public static bool FindTextInPdf(string fileFullPath, string text)
        {
            return GetTextFromPdf(fileFullPath).IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

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
                if (extractor.ExtractText().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
                return false;
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                return false;
            }

            return false;
        }
    }
}
