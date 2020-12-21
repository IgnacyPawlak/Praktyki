using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Again.Commands
{
    class SearchCSFile : ISearchCSFile
    {
        public string _filePath { get; set; }
        public string _content { get; set; }
        private List<string> _regexValues = new List<string>();

        public void Search(string _filePath)
        {
            //test
            _regexValues.Add("test");
            //tekst w cudzysłowiach
            _regexValues.Add("\".*\"");
            // teksty przypisane do zmiennych znakiem =
            _regexValues.Add("(?<= ( = )\").[^\",]+");

            var directories = Directory.EnumerateDirectories(_filePath).ToList();
            if (directories.Count > 0)
            {
                foreach (var item in directories)
                {
                    var filteredFiles = Directory.EnumerateFiles(_filePath).Where(file => file.EndsWith(".cs")).ToList();
                    foreach (var file in filteredFiles)
                    {
                        foreach (string regexValue in _regexValues)
                        {
                            Regex newRegex = new Regex(regexValue);
                            MatchCollection mc = newRegex.Matches(File.ReadAllText(file));
                            if (mc.Count > 0 && !(_content.Contains(file))) _content += file + "\n";
                        }                        
                    }
                    Search(item);
                }
            }
            else
            {
                var filteredFiles = Directory.EnumerateFiles(_filePath).Where(file =>file.EndsWith(".cs")).ToList();
                foreach (var file in filteredFiles)
                {
                    foreach (string regexValue in _regexValues)
                    {
                        Regex newRegex = new Regex(regexValue);
                        MatchCollection mc = newRegex.Matches(File.ReadAllText(file));
                        if (mc.Count > 0 && !(_content.Contains(file))) _content += file + "\n";
                    }
                }
            }
        }
    }
}
