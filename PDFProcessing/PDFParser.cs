using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Text;

namespace PDFRepositoryProject.PDFProcessing
{
    public class PDFParser
    {
        public static string ExtractTextFromPdf(byte[] file)
        {
            ITextExtractionStrategy its = new LocationTextExtractionStrategy();

            try
            {
                using (PdfReader reader = new PdfReader(file))
                {
                    StringBuilder text = new StringBuilder();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        var pageText = PdfTextExtractor.GetTextFromPage(reader, i, its);

                        string[] theLines = pageText.Split('\n');

                        foreach (var theLine in theLines)
                        {
                            text.AppendLine(theLine);
                        }
                    }

                    return text.ToString();
                }
            }
            catch(Exception ex)
            {
                return $"CAN'T PARSE PDF, ERROR:{ex}";
            }
        }
    }
}