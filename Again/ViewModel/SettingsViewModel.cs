using Again.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;

namespace Again.ViewModel
{
    public class SettingsViewModel:ViewModelBase
    {
        public SettingsViewModel()
        {
            SavePath = Properties.Settings.Default.SavePath;
            SelectedValue = Properties.Settings.Default.fromat;
        }
        private Browse browse = new Browse();

        string _savePath;

        public string SavePath { get { return _savePath; } set { this.Set(nameof(SavePath), ref _savePath, value); } }

        string _selectedValue;

        public string SelectedValue { get { return _selectedValue; } set { this.Set(nameof(SelectedValue), ref _selectedValue, value); } }

        public string SelectedValueShort { get { return SelectedValue.Split(':').Last(); } }

        public ICommand BrowseCommand => new RelayCommand(()=> ExecuteBrowseCommand());

        private void ExecuteBrowseCommand()
        {
            SavePath = browse.BrowseMethod();
        }

        public ICommand SaveSettingsCommand => new RelayCommand(() => ExecuteSaveSettingsCommand());

        private void ExecuteSaveSettingsCommand()
        {
            Properties.Settings.Default.SavePath = SavePath;
            Properties.Settings.Default.fromat = SelectedValue;
            Properties.Settings.Default.Save();
        }
    }
}
