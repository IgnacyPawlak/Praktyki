using GalaSoft.MvvmLight;
using Again.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Again.Commands;

namespace Again.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Browse sm = new Browse();
        private ViewModelBase _selectedViewModel;
        string _searchedValue;
        static string _filePath = @"C:\";
        string _content = "";
        bool _isCheckedSearchDown;
        bool _isCheckedSearchForSingleFile;
        bool _isCheckedCSFile;
        bool _isCheckedXMLFile;
        MatchCollection _matchCollection;
        private List<string> _regexValues = new List<string>();
        int _comboBoxSelectedIndex = 0;
        public List<string> RegexValues { get { return _regexValues; } private set { this.Set(nameof(RegexValues), ref _regexValues, value);}}
        public ResultsViewModel a { get; private set; }
        List<string> _searchCriteria = new List<string>();
        public List<string> SearchCriteria { get { return _searchCriteria; } set { this.Set(nameof(SearchCriteria), ref _searchCriteria, value); } }
        public MainViewModel()
        {
            a = new ResultsViewModel(this);
            RegexValues.Add("(?<=Content=\")(?!{Binding)[^\"]+");
            RegexValues.Add("(?<=Text=\")(?!{Binding)[^\"]+");
            RegexValues.Add("(?<=Name=\")(?!{Binding)[^\"]+");
        }
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { this.Set(nameof(SelectedViewModel), ref _selectedViewModel, value); }
        }

        public string SearchedValue
        {
            get { return _searchedValue; }
            set { this.Set(nameof(SearchedValue), ref _searchedValue, value); }
        }
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
        public MatchCollection InitialMatchCollection
        {
            get { return _matchCollection; }
            set { this.Set(nameof(InitialMatchCollection), ref _matchCollection, value); }
        }
        public ICommand SearchCommand => new RelayCommand(() => ExecuteSearchCommand());
        public ICommand BrowseCommand => new RelayCommand(() => { ExecuteBrowseCommand();  });
        public ICommand ComboBoxCommand => new RelayCommand(() => ExecuteComboBoxCommand());

        public bool IsCheckedCSFile { get { return _isCheckedCSFile; } set { this.Set(nameof(IsCheckedCSFile), ref _isCheckedCSFile, value); } }
        public bool IsCheckedXMLFile { get { return _isCheckedXMLFile; } set { this.Set(nameof(IsCheckedXMLFile), ref _isCheckedXMLFile, value); } }

        public int ComboBoxSelectedIndex { get { return _comboBoxSelectedIndex; } set { this.Set(nameof(ComboBoxSelectedIndex), ref _comboBoxSelectedIndex, value); }} 
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
            Content = string.Empty;
            if (_isCheckedSearchDown == true)
            {
                if(_isCheckedCSFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchCSFile();
                    searchFile.SearchDown();
                    Content = searchFile.Content;
                }
                else if(_isCheckedXMLFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchXMLFile();
                    searchFile.SearchDown();
                    Content = searchFile.Content;
                }
                else
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString());
                    searchFile.SearchDown();
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
                    Content = searchFile.Content;
                }
                else if (_isCheckedXMLFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString()).CreateSearchXMLFile();
                    searchFile.Search();
                    Content = searchFile.Content;
                }
                else
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, RegexValues[_comboBoxSelectedIndex].ToString());
                    searchFile.Search();
                    Content = searchFile.Content;
                }
            }
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
    }
}