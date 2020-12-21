using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Commands
{
    public interface ISearchCSFile
    {
        string _filePath { get; set; }
        string _content { get; set; }
        void Search(string _filePath);
    }
}
