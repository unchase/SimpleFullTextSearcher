using System;
using System.IO;
using System.IO.Packaging;
using System.Threading;
using Aspose.Cells;
using Aspose.Note;
using Aspose.OCR;
using Aspose.Slides;
using Code7248.word_reader;
using Fb2.Document;
using SautinSoft;
using SautinSoft.Document;
using VersOne.Epub;
using Cell = Aspose.Cells.Cell;
using LoadFormat = Aspose.Cells.LoadFormat;
using LoadOptions = Aspose.Cells.LoadOptions;

namespace SimpleFullTextSearcher.FileSearcher.Helpers
{
    public static class FullTextSearchHelper
    {
        public static bool FindTextInPdfFile(string fileFullPath, string text, ref CancellationTokenSource cts)
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

        public static bool FindTextInDocFile(string fileFullPath, string text)
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

        public static bool FindTextInDocumentFile(string fileFullPath, string text, ref CancellationTokenSource cts)
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

        public static bool FindTextInPresentationFile(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                // Instatiate Presentation class that represents a presentation file
                var pptxPresentation = new Presentation(fileFullPath);

                // Get an Array of ITextFrame objects from all slides in the presentation file
                var textFramesPptx = Aspose.Slides.Util.SlideUtil.GetAllTextFrames(pptxPresentation, true);

                //Loop through the Array of TextFrames
                foreach (var textFramePptx in textFramesPptx)
                {
                    if (cts.IsCancellationRequested)
                        break;

                    //Loop through paragraphs in current ITextFrame
                    foreach (var para in textFramePptx.Paragraphs)
                    {
                        if (cts.IsCancellationRequested)
                            break;

                        // Loop through portions in the current IParagraph
                        foreach (var port in para.Portions)
                        {
                            if (cts.IsCancellationRequested)
                                break;

                            if (port.Text.Contains(text))
                                return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static bool FindTextInOneNoteFile(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                // Load the document into Aspose.Note.
                Document oneFile = new Document(fileFullPath);

                // Retrieve text
                string oneNoteText = oneFile.GetText();

                return oneNoteText.Contains(text);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool FindTextInEpubFile(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                // Opens a book and reads all of its content into memory
                var epubBook = EpubReader.ReadBook(fileFullPath);

                // Enumerating the whole text content of the book in the order of reading
                foreach (var textContentFile in epubBook.ReadingOrder)
                {
                    if (cts.IsCancellationRequested)
                        break;

                    // HTML of current text content file
                    string htmlContent = textContentFile.Content;

                    if (htmlContent.Contains(text))
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static bool FindTextInFb2File(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                using (var fileStream = new FileStream(fileFullPath, FileMode.Open))
                {
                    var document = Fb2Document.CreateDocument();
                    document.Load(fileStream);
                    foreach (var documentBody in document.Bodies)
                    {
                        if (cts.IsCancellationRequested)
                            break;

                        foreach (var documentBodyContentNode in documentBody.Content)
                        {
                            if (cts.IsCancellationRequested)
                                break;

                            var contentNodeText = Fb2.Document.Extensions.XNodeExtension.GetNodeContent(documentBodyContentNode.ToXml());
                            if (contentNodeText.Contains(text))
                                return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static bool FindTextInPackageFile(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                using (Package fPackage = Package.Open(fileFullPath, FileMode.Open, FileAccess.Read))
                {
                    foreach (PackagePart fPart in fPackage.GetParts())
                    {
                        if (cts.IsCancellationRequested)
                            break;

                        using (var fPartStream = fPart.GetStream(FileMode.Open))
                        {
                            using (var textReader = new StreamReader(fPartStream))
                            {
                                var fPartText = textReader.ReadToEnd();
                                if (fPartText.Contains(text))
                                    return true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static bool FindTextInImageFile(string fileFullPath, string text, ref CancellationTokenSource cts)
        {
            try
            {
                // Create an instance of OcrEngine class
                var ocr = new OcrEngine();

                // Set the Image property of OcrEngine by reading an image file
                ocr.Image = ImageStream.FromFile(fileFullPath);

                // Set the RemoveNonText to true
                ocr.Config.DetectTextRegions = true;

                // Perform OCR operation
                if (ocr.Process())
                {
                    if (ocr.Text.ToString().Contains(text))
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static bool FindTextInExcellFile(string fileFullPath, string text, ref CancellationTokenSource cts)
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
                        break;

                    foreach (Cell cell in sheet.Cells)
                    {
                        if (cts.IsCancellationRequested)
                            break;

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
