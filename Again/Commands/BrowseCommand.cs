using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Again.ViewModel;

namespace Again.Model
{
    public class Browse
    {
        public string searchedValue = "Wpisz szukany tekst";
        public string filePath= @"D:\Praktyki\Again";
        public string content;
        public bool isChecked = false;
        public Regex regex = new Regex("test");
        public MatchCollection matchCollection;        
        public string BrowseMethod()
        {
                System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string a = folderBrowserDialog1.SelectedPath;
                    return a;
                }else
                {
                    string b = "Niepoprawna ścieżka";
                    return b;
                }
        }
    }
}
