using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.Functions
{
    public class PopulateListOfFiles
    {
        public PopulateListOfFiles(string filePath)
        {
            FilePath = filePath;
        }
        public string FilePath { get; set; }
        public List<Files> PopulateListOfXAMLFilesFunction()
        {
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".xaml")).ToList();
            List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfCSFilesFunction()
        {
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".cs")).ToList();
            List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfFilesFunction()
        {
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".xaml")||file.EndsWith(".cs")).ToList();
            List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
    }
}
