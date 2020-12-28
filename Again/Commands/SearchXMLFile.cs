using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Again.Commands
{
    class SearchXMLFile: ISearchFile
    {
        public string FilePath { get; set; }
        public string Content { get; set; }
        public List<string> _regexValues { get; private set; } = new List<string>();
        private string helpingFilePath;
        public SearchXMLFile(string filePath, string content, string regexValue)
        {
            FilePath = filePath;
            Content = content;
            _regexValues.Add(regexValue);
            //tekst w cudzysłowiach
            //_regexValues.Add("\".*\"");
            //wartości przypisane do Headera
            //_regexValues.Add("(?<=Header=\")(?!{Binding)[^\"]+");
            //Name
            //_regexValues.Add("(?<=Name=\")(?!{Binding)[^\"]+");
            //wartości przypisane do textu z wyłączeniem bindingów
            //_regexValues.Add("(?<=Text=\")(?!{Binding)[^\"]+");
            //to co wyżej tylko dla Content
            //_regexValues.Add("(?<=Content=\")(?!{Binding)[^\"]+");
            //Content i text w jednym
            //_regexValues.Add("(?<=(Content|Text)=\")(?!{Binding)[^\"]+");
            //Słowa pomiędzy ><
            //_regexValues.Add("(?<=(>))[a-zA-Z_0-9.]*");
            // teksty przypisane do zmiennych znakiem =
            //_regexValues.Add("(?<= ( = )\").[^\",]+");
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
                    var filteredFiles = Directory.EnumerateFiles(helpingFilePath).Where(file => file.EndsWith(".xaml")).ToList();
                    foreach (var file in filteredFiles)
                    {
                        foreach (string regexValue in _regexValues)
                        {
                            Regex newRegex = new Regex(regexValue, RegexOptions.IgnoreCase|RegexOptions.IgnorePatternWhitespace);
                            MatchCollection mc = newRegex.Matches(File.ReadAllText(file));
                            if (mc.Count > 0 && !(Content.Contains(file)))
                            {
                                foreach (var match in mc)
                                {
                                    if(string.IsNullOrWhiteSpace(match.ToString())==false)
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
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".xaml")).ToList();
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
                            if (string.IsNullOrWhiteSpace(match.ToString()) == false)
                                Content += /*file + "\t" + */match + "\n";
                        }
                    }
                }
            }
        }
        public void SearchForContent()
        {
            //to co wyżej tylko dla Content
            _regexValues.Add("(?<=Content=\")(?!{Binding)[^\"]+");
        }
        public void SearchForText()
        {
            //wartości przypisane do textu z wyłączeniem bindingów
            _regexValues.Add("(?<=Text=\")(?!{Binding)[^\"]+");
        }
        public void SearchForName()
        {
            //Name
            _regexValues.Add("(?<=Name=\")(?!{Binding)[^\"]+");
        }
    }
}
