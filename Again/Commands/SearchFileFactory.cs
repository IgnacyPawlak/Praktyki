using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Commands
{
    public class SearchFileFactory : ISearchFile
    {
        public SearchFileFactory(string filePath, string content)
        {
            FilePath= filePath;
            Content = content;
            InitSearch();
        }

        void InitSearch()
        {
            SearchList.Add(new SearchCSFile(FilePath, Content));
            SearchList.Add(new SearchXMLFile(FilePath, Content));
        }

        public string Content { get; set; }
        public string FilePath { get; set; }

        public void Search(string _filePath)
        {
        }
        public ISearchFile CreateSearchCSFile()
        {
            return new SearchCSFile(FilePath,Content);
        }
        public ISearchFile CreateSearchXMLFile()
        {
            return new SearchXMLFile(FilePath, Content);
        }

        List<ISearchFile> SearchList=new List<ISearchFile>();

        public void Search()
        {
            foreach (var item in SearchList)
            {
                item.Search();
            }

        }
    }
}
