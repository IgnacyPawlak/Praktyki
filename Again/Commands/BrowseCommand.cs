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
        public string BrowseForSingleFileMethod()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if(result==DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                return filePath;
            }
            else
            {
                string filePath = @"C:\";
                return filePath;
            }
        }
    }
}
