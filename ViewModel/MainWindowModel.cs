using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComputerBlocker
{
   
    public class MainWindowModel : INotifyPropertyChanged
    {
        private ICommand _changePageHost;
        private ICommand _changePageSettings;
        private ICommand _changePageHome;
        List<ObservableObject> view = new List<ObservableObject>();
        public MainWindowModel()
        {
            view.Add(new HomeWindowModel());
            view.Add(new SettingsViewModel());
            view.Add(new HostWindowModel());
            SubViewModel = view[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private object subViewModel;
        public object SubViewModel
        {
            get { return subViewModel; }
            set
            {
                if (Equals(subViewModel, value))
                    return;
                subViewModel = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand ChangePageSettings
        {
            get
            {
                if (_changePageSettings == null)
                {
                    _changePageSettings = new RelayCommand(
                        p => SubViewModel = view[1]);
                }

                return _changePageSettings;
            }
        }
        public ICommand ChangePageHome
        {
            get
            {
                if (_changePageHome == null)
                {
                    _changePageHome = new RelayCommand(
                        p => SubViewModel = view[0]);
                }

                return _changePageHome;
            }
        }
        public ICommand ChangePageHost
        {
            get
            {
                if (_changePageHost == null)
                {
                    _changePageHost = new RelayCommand(
                        p => SubViewModel = view[2]);
                }

                return _changePageHost;
            }
        }
    }
}
