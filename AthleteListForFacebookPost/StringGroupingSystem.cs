using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteListForFacebookPost
{
    internal class StringGroupingSystem
    {
        public static void GroupBy(List<string> competition, int number)
        {
            if (number == 1)
            {
                string athleteName = "";
                foreach (string start in competition)
                {
                    string xd = start.Replace("mężczyzn", "").Replace("kobiet", "");
                    string rest = string.Join(" ", xd.Split(" ").Skip(2));
                    if (athleteName != String.Join(" ", xd.Split(" ")[0..2]))
                    {
                        athleteName = String.Join(" ", xd.Split(" ")[0..2]);
                    }
                }
            }
            else
            {
                var groupedByDistance = competition.GroupBy(s => GetDistanceKey(s), (key, values) => new { Distance = key, Strings = values.ToList() });

                var groupedMen = groupedByDistance
                    .Select(group => new
                    {
                        Distance = group.Distance + " mężczyzn",
                        MenStrings = group.Strings.Where(s => s.Contains("mężczyzn")).ToList()
                    })
                    .Where(group => group.MenStrings.Any())
                    .ToList();
                var groupedWomen = groupedByDistance
                    .Select(group => new
                    {
                        Distance = group.Distance + " kobiet",
                        MenStrings = group.Strings.Where(s => s.Contains("kobiet")).ToList()
                    })
                    .Where(group => group.MenStrings.Any())
                    .ToList();
                var mergedGenders = groupedWomen.Concat(groupedMen).OrderBy(kvp => kvp.Distance).ToDictionary(pair => pair.Distance, pair => pair.MenStrings);
                foreach (var gender in mergedGenders)
                {
                    Console.WriteLine(gender.Key);
                    foreach (var dist in gender.Value)
                    {
                        Console.WriteLine(dist.Replace("mężczyzn", "").Replace("kobiet", ""));
                    }
                    Console.WriteLine();
                }
            }
        }
        static string GetDistanceKey(string str) => String.Join(" ", str.Split(" ")[2..4]);
    }
}

