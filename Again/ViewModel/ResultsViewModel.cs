using Again.Functions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Again.ViewModel
{
    public class ResultsViewModel : ViewModelBase
    {
        public ResultsViewModel(MainViewModel baseViewModel)
        {
            MyBaseViewModel = baseViewModel;
            HighlightTermBehavior = new HighlightTermBehavior();
        }

        public MainViewModel MyBaseViewModel { get; private set; }
        public HighlightTermBehavior HighlightTermBehavior { get; set; }
        int _selectedTerm;
        public int SelectedTerm { get { return _selectedTerm; } set { this.Set(nameof(SelectedTerm), ref _selectedTerm, value);} }
        
    }
}
