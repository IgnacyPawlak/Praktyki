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
        private string helpingFilePath;
        public List<Files> populatedListOfFiles = new List<Files>();
        public List<Files> PopulateListOfXAMLFilesFunction()
        {
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".xaml")).ToList();
            //List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                bool containsThisFile = false;
                foreach (var fileName in populatedListOfFiles)
                {
                    if (fileName.FilePath == files.FilePath) containsThisFile = true;
                }
                if (containsThisFile == false)
                    populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfXAMLFilesFunction(string filePath)
        {
            var filteredFiles = Directory.EnumerateFiles(filePath).Where(file => file.EndsWith(".xaml")).ToList();
            //List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                bool containsThisFile = false;
                foreach (var fileName in populatedListOfFiles)
                {
                    if (fileName.FilePath == files.FilePath) containsThisFile = true;
                }
                if (containsThisFile == false)
                    populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfCSFilesFunction()
        {
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".cs")).ToList();
            //List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                bool containsThisFile = false;
                foreach (var fileName in populatedListOfFiles)
                {
                    if (fileName.FilePath == files.FilePath) containsThisFile = true;
                }
                if (containsThisFile == false) populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfCSFilesFunction(string filePath)
        {
            var filteredFiles = Directory.EnumerateFiles(filePath).Where(file => file.EndsWith(".cs")).ToList();
            //List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                bool containsThisFile = false;
                foreach (var fileName in populatedListOfFiles)
                {
                    if (fileName.FilePath == files.FilePath) containsThisFile = true;
                }
                if (containsThisFile == false) populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfFilesFunction()
        {
            var filteredFiles = Directory.EnumerateFiles(FilePath).Where(file => file.EndsWith(".xaml")||file.EndsWith(".cs")).ToList();
            //List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                bool containsThisFile = false;
                foreach (var fileName in populatedListOfFiles)
                {
                    if (fileName.FilePath == files.FilePath) containsThisFile = true;
                }
                if (containsThisFile == false) populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfFilesFunction(string filePath)
        {
            var filteredFiles = Directory.EnumerateFiles(filePath).Where(file => file.EndsWith(".xaml") || file.EndsWith(".cs")).ToList();
            //List<Files> populatedListOfFiles = new List<Files>();
            foreach (var item in filteredFiles)
            {
                Files files = new Files(item, File.ReadAllText(item));
                bool containsThisFile = false;
                foreach (var fileName in populatedListOfFiles)
                {
                    if (fileName.FilePath == files.FilePath) containsThisFile = true;
                }
                if (containsThisFile == false) populatedListOfFiles.Add(files);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfCSFilesDownFunction()
        {
            if (helpingFilePath == null)
                helpingFilePath = FilePath;
            var directories = Directory.EnumerateDirectories(helpingFilePath).ToList();
            if (directories.Count > 0)
            {
                foreach (var item in directories)
                {
                    var filteredFiles = Directory.EnumerateFiles(helpingFilePath).Where(file => file.EndsWith(".cs")).ToList();
                    foreach (var file in filteredFiles)
                    {
                        Files files = new Files(file, File.ReadAllText(file));
                        bool containsThisFile = false;
                        foreach (var fileName in populatedListOfFiles)
                        {
                            if (fileName.FilePath == files.FilePath) containsThisFile = true;
                        }
                        if (containsThisFile == false) populatedListOfFiles.Add(files);
                    }
                    helpingFilePath = item;
                    PopulateListOfCSFilesDownFunction();
                }
            }
            else
            {
                PopulateListOfCSFilesFunction(helpingFilePath);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfXAMLFilesDownFunction()
        {
            if (helpingFilePath == null)
                helpingFilePath = FilePath;
            var directories = Directory.EnumerateDirectories(helpingFilePath).ToList();
            if (directories.Count > 0)
            {
                foreach (var item in directories)
                {
                    var filteredFiles = Directory.EnumerateFiles(helpingFilePath).Where(file => file.EndsWith(".xaml")).ToList();
                    foreach (var file in filteredFiles)
                    {
                        Files files = new Files(file, File.ReadAllText(file));
                        bool containsThisFile = false;
                        foreach (var fileName in populatedListOfFiles)
                        {
                            if (fileName.FilePath == files.FilePath)
                                containsThisFile = true;
                        }
                        if (containsThisFile == false) populatedListOfFiles.Add(files);
                    }
                    helpingFilePath = item;
                    PopulateListOfXAMLFilesDownFunction();
                }
            }
            else
            {
                PopulateListOfXAMLFilesFunction(helpingFilePath);
            }
            return populatedListOfFiles;
        }
        public List<Files> PopulateListOfFilesDownFunction()
        {
            if (helpingFilePath == null)
                helpingFilePath = FilePath;
            var directories = Directory.EnumerateDirectories(helpingFilePath).ToList();
            if (directories.Count > 0)
            {
                foreach (var item in directories)
                {
                    var filteredFiles = Directory.EnumerateFiles(helpingFilePath).Where(file => file.EndsWith(".xaml") || file.EndsWith(".cs")).ToList();
                    foreach (var file in filteredFiles)
                    {
                        Files files = new Files(file, File.ReadAllText(file));
                        bool containsThisFile=false;
                        foreach (var fileName in populatedListOfFiles)
                        {
                            if (fileName.FilePath == files.FilePath) containsThisFile = true;
                        }
                        if(containsThisFile==false) populatedListOfFiles.Add(files);
                    }
                    helpingFilePath = item;
                    PopulateListOfFilesDownFunction();
                }
            }
            else
            {
                PopulateListOfFilesFunction(helpingFilePath);
            }
            return populatedListOfFiles;
        }
    }
}
