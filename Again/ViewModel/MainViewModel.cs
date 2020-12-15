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
        private SearchDown sd = new SearchDown();
        private Search s = new Search();
        private ViewModelBase _selectedViewModel;

        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { this.Set(nameof(SelectedViewModel), ref _selectedViewModel, value); }
        }

        string _searchedValue;
        public string SearchedValue
        {
            get { return _searchedValue; }
            set { this.Set(nameof(SearchedValue), ref _searchedValue, value); }
        }
        string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { this.Set(nameof(FilePath), ref _filePath, value); }
        }
        string _content;
        public string Content
        {
            get { return _content; }
            set {this.Set(nameof(Content), ref _content, value);}
        }
        bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { this.Set(nameof(IsChecked), ref _isChecked, value); }
        }
        MatchCollection _matchCollection;
        public MatchCollection InitialMatchCollection
        {
            get { return _matchCollection; }
            set { this.Set(nameof(InitialMatchCollection), ref _matchCollection, value); }
        }
        public ICommand SearchCommand => new RelayCommand(() =>
        {
            if (_isChecked == true)
            {
                sd.SearchDownDirectory(_filePath); Content = sd.content;
            }
            else { s.SearchCommand(_filePath); Content = s.content; }
        });
        public ICommand BrowseCommand => new RelayCommand(() => { FilePath = sm.BrowseMethod();  });
    }
}