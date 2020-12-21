using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Commands
{
    public class SearchXMLFileFactory : ISearchFile
    {
        public SearchXMLFileFactory(string filePath, string content)
        {
            FilePath = filePath;
            Content = content;
        }

        public string FilePath { get  ; set  ; }
        public string Content { get  ; set  ; }

        public void Search()
        {
        }
        public ISearchFile CreateSearchXMLFile()
        {
            return new SearchXMLFile(FilePath, Content);
        }
    }
}
