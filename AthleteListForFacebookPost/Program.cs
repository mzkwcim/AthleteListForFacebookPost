using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using AthleteListForFacebookPost;
using static System.Net.Mime.MediaTypeNames;



namespace AthleteListForFacebookPost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] textToOperatOn = PDFReader.GetTextFromPdf();
            List<string> chunksOfText = [];
            string oneChunkOfText = "";
            Console.WriteLine("Chcesz pogrupować wyniki po zawodnikach czy po dystansach? (1 - zawodnicy, 2 - dystanse)");
            int number = Int32.Parse(Console.ReadLine());
            Console.Clear();
            Thread.Sleep(1000);
            for (int i = 0; i < textToOperatOn.Length; i++)
            {
                if (Regex.IsMatch(textToOperatOn[i], @"\w+\s+\w+,\s+\d+\s+") || i == textToOperatOn.Length - 1)
                {
                    chunksOfText.Add(oneChunkOfText);
                    oneChunkOfText = "";
                }
                oneChunkOfText += textToOperatOn[i] + "\n";
            }
            StringGroupingSystem.GroupBy(StringSelectingSystem.SelectImportantString(chunksOfText), number);
            Console.ReadLine();
        }

    }
}
