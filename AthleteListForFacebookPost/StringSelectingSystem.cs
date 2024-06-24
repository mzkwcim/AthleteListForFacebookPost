using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AthleteListForFacebookPost
{
    internal class StringSelectingSystem
    {
        public static List<string> SelectImportantString(List<string> chunksOfText)
        {
            int adder = 0;
            List<string> competition = new List<string>();
            foreach (string chunk in chunksOfText)
            {
                string name = "";
                foreach (string c in chunk.Split("\n"))
                {
                    string swimmingEvent = "";
                    if (Regex.IsMatch(c, @"\w+,\s+\d+") || Regex.IsMatch(c, @"\d+\w\s"))
                    {
                        string[] distance = Regex.Replace(c, @"%.+", "").Split(" ");
                        if (c.Contains('%'))
                        {
                            swimmingEvent = $"{distance[0]} {distance[1]}";
                            competition.Add($"{name} {swimmingEvent} {StringOperator.ArabicToRomanianNumbers(Convert.ToInt32(distance[5].Replace(".", "")))} miejsce {distance[^5]} {StringOperator.IsPersonalBest(distance[^1])} {GetGender(name)}");
                        }
                        else if ((adder == 0 && !c.Contains('%')) || c.Contains("lat"))
                        {
                            name = StringOperator.ToTitleString(c.Substring(0, (c.IndexOf(',', c.IndexOf(',') + 1) == -1 ? c.IndexOf(',') : c.IndexOf(',', c.IndexOf(',') + 1))));
                        }
                        else if (distance.Length > 5 && distance[5] != "-" && !distance[5].Contains("SKREŚL") && !distance[6].Contains("SKREŚL"))
                        {
                            swimmingEvent = $"{distance[0]} {distance[1]}";
                            competition.Add($"{name} {swimmingEvent} {StringOperator.ArabicToRomanianNumbers(Convert.ToInt32(distance[5].Replace(".", "")))} miejsce {distance[6]} r.ż. {GetGender(name)}");
                        }
                    }
                    adder++;
                }
                adder = 0;
            }
            return competition;
        }
        private static string GetGender(string name) => (name[name.Length - 1] == 'a') ? "kobiet" : "mężczyzn";
    }
}
