using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Commands
{
    public class SearchCSFileFactory : ISearchCSFile
    {
        public SearchCSFileFactory(string _filePath)
        {
            this._filePath = _filePath;
        }

        public string _content { get; set; }
        public string _filePath { get; set; }

        public void Search(string _filePath)
        {
        }
        public ISearchCSFile CreateSearchCSFile()
        {
            return new SearchCSFile();
        }
    }
}
