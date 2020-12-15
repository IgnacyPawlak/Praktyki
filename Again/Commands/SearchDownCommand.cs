using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Again.Commands
{
    public class SearchDown
    {

        public string content = "";
        public void SearchDownDirectory(string directoryPath)
        {
            Regex regex = new Regex("test");
            var directories = Directory.EnumerateDirectories(directoryPath).ToList();
            if (directories.Count > 0)
            {
                foreach (var item in directories)
                {
                    var filteredFiles = Directory
                        .EnumerateFiles(directoryPath)
                        .Where(file => file.ToLower().EndsWith(".xml") || file.EndsWith(".cs"))
                        .ToList();
                    foreach (var file in filteredFiles)
                    {
                        MatchCollection mc = regex.Matches(File.ReadAllText(file));
                        if (mc.Count > 0&&!(content.Contains(file))) content += file + "\n";
                    }
                    SearchDownDirectory(item);
                }
            }
            else
            {
                var filteredFiles = Directory
                        .EnumerateFiles(directoryPath)
                        .Where(file => file.ToLower().EndsWith(".xml") || file.EndsWith(".cs"))
                        .ToList();
                foreach (var file in filteredFiles)
                {
                    MatchCollection mc = regex.Matches(File.ReadAllText(file));
                    if (mc.Count > 0&&!(content.Contains(file))) content += file + "\n";
                }
            }
        }
    }
}
