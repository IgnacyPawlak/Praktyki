using GalaSoft.MvvmLight;
using Again.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Again.Commands;
using Again.Functions;

namespace Again.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Browse sm = new Browse();

        static string _filePath = @"D:\Praktyki\repo\Again";

        string _content = "";

        bool _isCheckedSearchDown;

        bool _isCheckedSearchForSingleFile;

        bool _isCheckedCSFile;

        bool _isCheckedXMLFile;

        bool _searchButtonClicked;

        private List<string> _regexValues = new List<string>();

        int _comboBoxSelectedIndex = 0;

        int _listViewSelectedIndex = 0;

        List<string> _searchCriteria = new List<string>();

        List<Files> _listOfFiles = new List<Files>();

        Files _listViewSelectedItem;

        List<string> _foundRegexMatches = new List<string>();

        public bool SearchButtonClicked { get { return _searchButtonClicked; } set { this.Set(nameof(SearchButtonClicked), ref _searchButtonClicked, value); } }

        public Files ListViewSelectedItem { get { return _listViewSelectedItem; } set { this.Set(nameof(ListViewSelectedItem), ref _listViewSelectedItem, value); } }

        public List<Files> ListOfFiles { get { return _listOfFiles; } private set { this.Set(nameof(ListOfFiles), ref _listOfFiles, value); } }

        public List<string> RegexValues { get { return _regexValues; } private set { this.Set(nameof(RegexValues), ref _regexValues, value);} }

        public List<string> SearchCriteria { get { return _searchCriteria; } set { this.Set(nameof(SearchCriteria), ref _searchCriteria, value); } }

        public List<string> FoundRegexMatches { get { return _foundRegexMatches; } set { this.Set(nameof(FoundRegexMatches), ref _foundRegexMatches, value); } }

        public string pattern { get; } = "\".*\"";

        public string FilePath
        {
            get { return _filePath; }
            set { this.Set(nameof(FilePath), ref _filePath, value); }
        } 

        public string Content
        {
            get { return _content; }
            set {this.Set(nameof(Content), ref _content, value);}
        }

        public bool IsCheckedSearchDown
        {
            get { return _isCheckedSearchDown; }
            set { this.Set(nameof(IsCheckedSearchDown), ref _isCheckedSearchDown, value); }
        }

        public bool IsCheckedSearchForSingleFile
        {
            get { return _isCheckedSearchForSingleFile; }
            set { this.Set(nameof(IsCheckedSearchForSingleFile), ref _isCheckedSearchForSingleFile, value); }
        }

        public bool IsCheckedCSFile { get { return _isCheckedCSFile; } set { this.Set(nameof(IsCheckedCSFile), ref _isCheckedCSFile, value); } }

        public bool IsCheckedXMLFile { get { return _isCheckedXMLFile; } set { this.Set(nameof(IsCheckedXMLFile), ref _isCheckedXMLFile, value); } }

        public int ComboBoxSelectedIndex { get { return _comboBoxSelectedIndex; } set { this.Set(nameof(ComboBoxSelectedIndex), ref _comboBoxSelectedIndex, value); } } 

        public int ListViewSelectedIndex { get { return _listViewSelectedIndex; } set { this.Set(nameof(ListViewSelectedIndex), ref _listViewSelectedIndex, value); } }

        public ICommand SearchCommand => new RelayCommand(() => ExecuteSearchCommand());

        public ICommand BrowseCommand => new RelayCommand(() => { ExecuteBrowseCommand();  });

        public ICommand ComboBoxCommand => new RelayCommand(() => ExecuteComboBoxCommand());

        private void ExecuteBrowseCommand()
        {
            if(_isCheckedSearchForSingleFile)
            {
                FilePath = sm.BrowseForSingleFileMethod();
                if(FilePath.EndsWith(".xaml"))
                {
                    IsCheckedXMLFile = true;
                    ExecuteComboBoxCommand();
                }
            }
            else
            {
                FilePath = sm.BrowseMethod();
            }
        }

        private void ExecuteSearchCommand()
        {
            PopulateListOfFiles populateListOfFiles = new PopulateListOfFiles(FilePath);
            Content = string.Empty;
            if (_isCheckedSearchDown == true)
            {
                if(_isCheckedCSFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchCSFile();
                    searchFile.SearchDown();
                    ListOfFiles = populateListOfFiles.PopulateListOfCSFilesDownFunction();
                    Content = searchFile.Content;
                }
                else if(_isCheckedXMLFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchXMLFile();
                    searchFile.SearchDown();
                    ListOfFiles = populateListOfFiles.PopulateListOfXAMLFilesDownFunction();
                    Content = searchFile.Content;
                }
                else
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString());
                    searchFile.SearchDown();
                    ListOfFiles = populateListOfFiles.PopulateListOfFilesDownFunction();
                    Content = searchFile.Content;
                }                
            }
            else if(_isCheckedSearchForSingleFile)
            {
                if(FilePath.EndsWith(".xaml"))
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchXMLFile();
                    searchFile.SearchSingleFile();
                    Content = searchFile.Content;
                }
                else if(FilePath.EndsWith(".cs"))
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchCSFile();
                    searchFile.SearchSingleFile();
                    Content = searchFile.Content;
                }
            }
            else
            {
                if (_isCheckedCSFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchCSFile();
                    searchFile.Search();
                    ListOfFiles = populateListOfFiles.PopulateListOfCSFilesFunction();
                    Content = searchFile.Content;
                }
                else if (_isCheckedXMLFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchXMLFile();
                    ListOfFiles = populateListOfFiles.PopulateListOfXAMLFilesFunction();
                    searchFile.Search();
                    Content = searchFile.Content;
                }
                else
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString());
                    searchFile.Search();
                    ListOfFiles = populateListOfFiles.PopulateListOfFilesFunction();
                    Content = searchFile.Content;
                }
            }
            SearchButtonClicked = true;
        }

        private void ExecuteComboBoxCommand()
        {
            if (IsCheckedXMLFile)
            {
                if(!SearchCriteria.Contains("Content"))
                { 
                SearchCriteria.Add("Content");
                SearchCriteria.Add("Text");
                SearchCriteria.Add("Name");
                }
            }
            else
            {
            }
        }

        private void NewExecuteSearchCommand()
        {
            PopulateListOfFiles populateListOfFiles = new PopulateListOfFiles(FilePath);
            ListOfFiles = populateListOfFiles.PopulateListOfXAMLFilesFunction();
            SearchButtonClicked = true;
        }

        public MainViewModel()
        {
            
            RegexValues.Add("\".*\"");
            RegexValues.Add("(?<=Content=\")(?!{Binding)[^\"]+");
            RegexValues.Add("(?<=Text=\")(?!{Binding)[^\"]+");
            RegexValues.Add("(?<=Name=\")(?!{Binding)[^\"]+");            
        }
    }
}