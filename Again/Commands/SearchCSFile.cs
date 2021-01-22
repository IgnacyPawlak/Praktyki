using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Again.Commands
{
    class SearchCSFile : ISearchFile
    {
        public string FilePath { get; set; }
        public string Content { get; set; }
        public List<string> _regexValues { get; private set; } = new List<string>();
        private string helpingFilePath;

        public SearchCSFile(string filePath, string content)
        {
            FilePath = filePath;
            Content = content;
            //test
            //_regexValues.Add("test");
            //tekst pomiędzy pierwszym a ostatnim cudzysłowiem w linii <----- jak na razie najlepsza opcja
            //_regexValues.Add("\".*\"");
            // teksty przypisane do zmiennych znakiem =
            //_regexValues.Add("(?<= ( = )\").[^\",]+");
            //SŁOWA w cydzysłowiach
            //_regexValues.Add("((?<=\"([ ]+)?)(([ ]+)?[A-Za-z0-9_]+([ ])?)+[^\"]+)+");
            //znalezione na stackoverflow
            //string pattern = "([\"'])(?:(?=(\\?))\\2.)*?\\1";
            // dopasowuje tylko jedno wyrażenie w "" w linii
            string pattern = @"""[\w\s\.]+""";
            _regexValues.Add(pattern);
        }

        public void SearchDown()
        {
            if (helpingFilePath == null)
                helpingFilePath = FilePath;
            var directories = Directory.EnumerateDirectories(helpingFilePath).ToList();
            if (directories.Count > 0)
            {
                foreach (var item in directories)
                {
                    var filteredFiles = Directory.EnumerateFiles(helpingFilePath).Where(file => file.EndsWith(".cs")).ToList();
                    foreach (var file in filteredFiles)
                    {
                        foreach (string regexValue in _regexValues)
                        {
                            Regex newRegex = new Regex(regexValue, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                            MatchCollection mc = newRegex.Matches(File.ReadAllText(file));
                            if (mc.Count > 0 && !(Content.Contains(file)))
                            {
                                foreach (var match in mc)
                                {

                                    Content += /*file + "\t" + */match + "\n";
                                }
                            }
                        }
                    }
                    helpingFilePath = item;
                    SearchDown();
                }
            }
            else
            {
                Search();
            }
        }
        public void Search()
        {
            try
            {
                var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".cs")).ToList();
                foreach (var file in filteredFiles)
                {
                    foreach (string regexValue in _regexValues)
                    {
                        Regex newRegex = new Regex(regexValue, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace|RegexOptions.Multiline);
                        MatchCollection mc = newRegex.Matches(File.ReadAllText(file));
                        if (mc.Count > 0 && !(Content.Contains(file)))
                        {
                            foreach (var match in mc)
                            {

                                Content += /*file + "\t" + */match + "\n";
                            }

                        }
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }            
        }
        public void SearchSingleFile()
        {
            try
            {
                foreach (string regexValue in _regexValues)
                {
                    Regex newRegex = new Regex(regexValue, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
                    MatchCollection mc = newRegex.Matches(File.ReadAllText(FilePath));
                    if (mc.Count > 0 && !(Content.Contains(FilePath)))
                    {
                        foreach (var match in mc)
                        {

                            Content += /*file + "\t" + */match + "\n";
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
