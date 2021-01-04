using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Again.Functions
{
    public class Files
    {
        public Files(string filePath, string content)
        {
            string[] subs = filePath.Split('\\');
            Name = subs.Last();
            Content = content;
        }
        public string Name { get; set; }
        public string Content { get; set; }
        public void ColorText()
        {
            List<string> regexValues = new List<string>();
            string pattern = @"""[\w\s\.]+""";
            regexValues.Add(pattern);
            string basetext = Content;
            foreach (var regexValue in regexValues)
            {
                Regex newRegex = new Regex(regexValue, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
                MatchCollection mc = newRegex.Matches(basetext);
                foreach (string match in mc)
                {
                }
            }
        }
    }
}
