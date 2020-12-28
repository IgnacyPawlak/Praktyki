using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Commands
{
    public class SearchFileFactory : ISearchFile
    {
        public SearchFileFactory(string filePath, string content, string regexValue)
        {
            FilePath= filePath;
            Content = content;
            RegexValue = regexValue;
            InitSearch();
        }

        void InitSearch()
        {
            SearchList.Add(new SearchCSFile(FilePath, Content));
            SearchList.Add(new SearchXMLFile(FilePath, Content, RegexValue));
        }

        public string Content { get; set; }
        public string FilePath { get; set; }
        public string RegexValue { get; set; }
        public List<string> _regexValues { get; }

        public ISearchFile CreateSearchCSFile()
        {
            return new SearchCSFile(FilePath,Content);
        }
        public ISearchFile CreateSearchXMLFile()
        {
            return new SearchXMLFile(FilePath, Content, RegexValue);
        }

        List<ISearchFile> SearchList=new List<ISearchFile>();
        public void SearchDown()
        {
            foreach (var item in SearchList)
            {
                item.SearchDown();
                Content += item.Content;
            }
        }

        public void Search()
        {
            foreach (var item in SearchList)
            {
                item.Search();
                Content += item.Content;
            }

        }
    }
}
