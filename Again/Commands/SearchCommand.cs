using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Again.Commands
{
    public class Search
    {
        public string content = "";
        public void SearchCommand(string directoryPath)
        {
            Regex regex = new Regex("test");
            var filteredFiles = Directory
                    .EnumerateFiles(directoryPath)
                    .Where(file => file.ToLower().EndsWith(".xml") || file.EndsWith(".cs"))
                    .ToList();
            foreach (var file in filteredFiles)
            {
                MatchCollection mc = regex.Matches(File.ReadAllText(file));
                if (mc.Count > 0 && !(content.Contains(file))) content += file + "\n";
            }
        }
    }
}
