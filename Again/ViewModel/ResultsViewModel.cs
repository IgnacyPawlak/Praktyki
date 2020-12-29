using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.ViewModel
{
    public class ResultsViewModel:ViewModelBase
    {
        public ResultsViewModel(MainViewModel baseViewModel)
        {
            MyBaseViewModel = baseViewModel;
        }

        public MainViewModel MyBaseViewModel { get; private set; }
        
    }
}
