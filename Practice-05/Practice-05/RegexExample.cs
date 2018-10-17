using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice_05
{
    public class RegexExample
    {

        public void RunExample()
        {
            string value = "25";

            Console.WriteLine(Regex.IsMatch(value, @"\d+"));
            var match = Regex.Match(value, @"\d");
            if (match.Success)
            {
                Console.WriteLine(match.Value);
            }

            match = match.NextMatch();
            if (match.Success)
            {
                Console.WriteLine(match.Value);
            }

            value = "25 bla 65";
            match = Regex.Match(value, @"(\d+)\s[a-z]+\s(\d+)");
            if (match.Success)
            {
                Console.WriteLine(match.Groups.Count);
                Console.WriteLine(match.Groups[0].Value);
                Console.WriteLine(match.Groups[1].Value);
                Console.WriteLine(match.Groups[2].Value);
            }

        }
    }
}
