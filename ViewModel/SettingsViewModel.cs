using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerBlocker
{
    public class SettingsViewModel : ValidatableViewModelBase
    {
        private string _currentPassword;
        public SettingsViewModel() {
            SettingsModel.Password = 1234.ToString();
        }
        public string Password
        {
            get { return _currentPassword; }
            set
            {
                if (SettingsModel.Password == null)
                    SettingsModel.Password = 1234.ToString();
                if (_currentPassword == value)
                    return;
                _currentPassword = value;
                RaisePropertyChanged("Password"); // Сообщаем о том, что поле изменилось
            }
        }

        public void CreatePassword(object obj) { 
            SettingsModel.Password = Password;
            Password = string.Empty;
        }
        private ICommand _initPass;
        public ICommand InitPass
        {
            get
            {
                if (_initPass == null)
                {
                    _initPass = new RelayCommand(CreatePassword);
                }

                return _initPass;
            }
        }
        protected override string Validate(string columnName)
        {
            if (columnName == nameof(Password))
            {
                if (String.IsNullOrWhiteSpace(Password))
                {
                    ButtonCreatePassEnabled = false;
                    return "Строка не заполнена";
                }
                ButtonCreatePassEnabled = true;
            }

            return string.Empty;
        }
        private Boolean _buttonCreatePass_enabled;
        public Boolean ButtonCreatePassEnabled
        {
            get { return _buttonCreatePass_enabled; }
            set
            {
                _buttonCreatePass_enabled = value;
                OnPropertyChanged("ButtonCreatePassEnabled");
            }
        }
    }
}
