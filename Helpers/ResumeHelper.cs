
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;
namespace Duffl_career.Helpers {
    public class ResumeHelper
    {
        public static string ExtractText(string path)
        {
            var sb = new StringBuilder();

            using (PdfReader reader = new PdfReader(path))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    sb.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
                }
            }

            return sb.ToString();
        }
    }
}

