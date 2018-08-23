using System;
using System.IO;
using System.Threading;
using Aspose.Cells;
using Code7248.word_reader;
using SautinSoft;
using SautinSoft.Document;
using LoadOptions = Aspose.Cells.LoadOptions;

namespace SimpleFullTextSearcher.FileSearcher.Helpers
{
    public static class FullTextSearchHelper
    {
        public static bool FindTextInPdf(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            var pdfFocus = new PdfFocus();
            try
            {
                pdfFocus.OpenPdf(fileFullPath);
                if (pdfFocus.PageCount > 0)
                {
                    for (var i = 1; i < pdfFocus.PageCount + 1; i++)
                    {
                        if (cts.IsCancellationRequested)
                            break;
                        if (pdfFocus.ToText(i, i).IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            pdfFocus.ClosePdf();
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                pdfFocus.ClosePdf();
            }

            return false;
        }

        public static bool FindTextInDoc(string fileFullPath, string text)
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

        public static bool FindTextInDocxHtml(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                var dc = DocumentCore.Load(fileFullPath);
                foreach (Run run in dc.GetChildElements(true, ElementType.Run))
                {
                    if (cts.IsCancellationRequested)
                        break;

                    if (run.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static bool FindTextInExcell(string fileFullPath, string text, ref CancellationTokenSource cts)
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
                    if (cts.IsCancellationRequested)
                        return false;
                    foreach (Cell cell in sheet.Cells)
                    {
                        if (cts.IsCancellationRequested)
                            return false;

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
