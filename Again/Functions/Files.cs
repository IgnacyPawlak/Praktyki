using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Functions
{
    public class Files
    {
        public Files(string filePath, string content)
        {
            string[] subs = filePath.Split('\\');
            Name = subs.Last();
            Content = content;
        }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
