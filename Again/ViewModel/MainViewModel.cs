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
        //private readonly static ResultsViewModel _resultsViewModel = new ResultsViewModel();
        private Browse sm = new Browse();
        private ViewModelBase _selectedViewModel;
        string _searchedValue;
        static string _filePath = @"C:\";
        string _content = "";
        bool _isChecked;
        MatchCollection _matchCollection;
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
        private void ExecuteSearchCommand()
        {
            Content = string.Empty;
            if (_isChecked == true)
            {
                ISearchFile searchFile = new SearchFileFactory(_filePath, _content);
                searchFile.SearchDown();
                Content = searchFile.Content;
                //sd.SearchDownDirectory(_filePath); Content = sd.content;
            }
            else
            {
                ISearchFile searchFile = new SearchFileFactory(_filePath, _content);
                searchFile.Search();
                Content = searchFile.Content;
                //s.SearchCommand(_filePath); Content = s.content;
            }
        }
    }
}