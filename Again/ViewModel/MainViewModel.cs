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
        bool _isChecked;
        bool _isCheckedCSFile;
        bool _isCheckedXMLFile;
        MatchCollection _matchCollection;
        public List<string> _regexValues = new List<string>();
        string _comboBoxSelectedItem;
        public List<string> RegexValues { get { return _regexValues; } private set { this.Set(nameof(RegexValues), ref _regexValues, value);}}
        public ResultsViewModel a { get; private set; }
        public MainViewModel()
        {
            a = new ResultsViewModel(this);
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
        public bool IsChecked
        {
            get { return _isChecked; }
            set { this.Set(nameof(IsChecked), ref _isChecked, value); }
        }
        public MatchCollection InitialMatchCollection
        {
            get { return _matchCollection; }
            set { this.Set(nameof(InitialMatchCollection), ref _matchCollection, value); }
        }
        public ICommand SearchCommand => new RelayCommand(() => ExecuteSearchCommand());
        public ICommand BrowseCommand => new RelayCommand(() => { FilePath = sm.BrowseMethod();  });
        public ICommand ComboBoxCommand => new RelayCommand(() => ExecuteComboBoxCommand());

        public bool IsCheckedCSFile { get { return _isCheckedCSFile; } set { this.Set(nameof(IsCheckedCSFile), ref _isCheckedCSFile, value); } }
        public bool IsCheckedXMLFile { get { return _isCheckedXMLFile; } set { this.Set(nameof(IsCheckedXMLFile), ref _isCheckedXMLFile, value); } }

        public string ComboBoxSelectedItem { get { return _comboBoxSelectedItem; } set { this.Set(nameof(ComboBoxSelectedItem), ref _comboBoxSelectedItem, value); }}

        private void ExecuteSearchCommand()
        {
            Content = string.Empty;
            if (_isChecked == true)
            {
                if(_isCheckedCSFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, _comboBoxSelectedItem).CreateSearchCSFile();
                    searchFile.SearchDown();
                    Content = searchFile.Content;
                }
                else if(_isCheckedXMLFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, _comboBoxSelectedItem).CreateSearchXMLFile();
                    searchFile.SearchDown();
                    Content = searchFile.Content;
                }
                else
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, _comboBoxSelectedItem);
                    searchFile.SearchDown();
                    Content = searchFile.Content;
                }                
            }
            else
            {
                if (_isCheckedCSFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, _comboBoxSelectedItem).CreateSearchCSFile();
                    searchFile.Search();
                    Content = searchFile.Content;
                }
                else if (_isCheckedXMLFile)
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, _comboBoxSelectedItem).CreateSearchXMLFile();
                    searchFile.Search();
                    Content = searchFile.Content;
                }
                else
                {
                    ISearchFile searchFile = new SearchFileFactory(_filePath, _content, _comboBoxSelectedItem);
                    searchFile.Search();
                    Content = searchFile.Content;
                }
            }
        }
        private void ExecuteComboBoxCommand()
        {
            if (_isCheckedXMLFile)
            {
                RegexValues.Add("(?<=Content=\")(?!{Binding)[^\"]+");
                RegexValues.Add("(?<=Text=\")(?!{Binding)[^\"]+");
                RegexValues.Add("(?<=Name=\")(?!{Binding)[^\"]+");
            }
            else
            {
                RegexValues.Add("else");
            }
        }
    }
}