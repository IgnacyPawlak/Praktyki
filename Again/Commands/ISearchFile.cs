using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Commands
{
    public interface ISearchFile
    {
        string FilePath { get; set; }
        string Content { get; set; }
        void Search();
        void SearchDown();
    }
}
