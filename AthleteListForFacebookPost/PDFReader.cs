using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace AthleteListForFacebookPost
{
    internal class PDFReader
    {
        public static string[] GetTextFromPdf()
        {
            Console.WriteLine("Podaj pełną ścieżkę do pliku: ");
            string newPath = Console.ReadLine();
            Console.WriteLine("Wprowadź pełną nazwę twojego klubu");
            string yourClubName = Console.ReadLine();
            Console.WriteLine("Wprowadź pełną nazwę klubu, który znajduje się po twoim klubie w wynikach");
            string theNextClub = Console.ReadLine();
            StringBuilder text = new();
            using (PdfReader pdfReader = new(newPath))
            {
                using (PdfDocument pdfDocument = new(pdfReader))
                {
                    for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pageNumber), strategy);
                        text.Append(pageText);
                    }
                }
            }
            Console.WriteLine("Na starcie " + text.ToString().Split('\n')[1]);
            string convertedText = text.ToString();
            int startIndex = convertedText.IndexOf(yourClubName);
            int endIndex = convertedText.IndexOf(theNextClub);
            string[] resultString = Regex.Split(Regex.Replace(convertedText.Substring(startIndex, endIndex - startIndex), @" - Strona \d+", ""), @"\n");
            return resultString;
        }
    }
}
