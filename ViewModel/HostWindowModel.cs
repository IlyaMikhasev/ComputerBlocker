using ComputerBlocker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerBlocker
{
    public class HostWindowModel :  ValidatableViewModelBase
    {
        /// <summary>
        /// Команда для добавления компьютера в список
        /// </summary>
        private ICommand AddComputerCommand { get; set; }
        
        private ICommand _removeComputer;
        private ICommand _blockedComputer;
        
        public ObservableCollection<Computer> Computers { get; set; }
        public string _ip;
        public string IP
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }
        private IPAddress _ipAddress;
        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public HostWindowModel() {
            Computers = HostModel.GetComputers();
            //при запуске добавляет свой компьютер в список
            HostModel.AddComputer(new Computer(Dns.GetHostByName(Dns.GetHostName()).AddressList[0]));
            ButtonAddEnabled = false;
            ButtonRemoveEnabled = false;
        }
       /// <summary>
       /// Удаление компьютера из списка
       /// </summary>
       /// <param name="obj"></param>
        public void RemoveComputerCollection(object obj) {
            HostModel.RemoveComputer(_selectedItem);
        }
        public ICommand RemoveComputer
        {
            get
            {
                if (_removeComputer == null)
                {
                    _removeComputer = new RelayCommand(RemoveComputerCollection);
                }

                return _removeComputer;
            }
        }
        /// <summary>
        /// Проверка строки ip address
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected override string Validate(string columnName)
        {
            if (columnName == nameof(IP))
            {
                if (String.IsNullOrWhiteSpace(IP))
                {
                    return "Строка не заполнена";
                }
                if (!IPAddress.TryParse(IP.Replace(",","."), out var value))
                {
                    return "Строка заполнена не верно";
                }
                _ipAddress = value;
                if (HostModel.equelsComputerToData(_ipAddress))
                {
                    return "Компьютер с этим IP Address-ом уже в списке";
                }
                ButtonAddEnabled = true;
                ButtonRemoveEnabled = true;
            }

            return string.Empty;
        }
        /// <summary>
        /// Добавление компьютера
        /// </summary>
        /// <param name="obj"></param>
        private void AddComputerCollection(object obj)
        {
            ButtonAddEnabled = false;
            foreach (var comp in Computers)
                if (comp.IpAddress == _ipAddress)
                    return;
            HostModel.AddComputer(new Computer(_ipAddress));
        }
        public ICommand AddComputer
        {
            get
            {
                if (AddComputerCommand == null)
                {
                    AddComputerCommand = new RelayCommand(AddComputerCollection);
                }

                return AddComputerCommand;
            }
        }
        /// <summary>
        /// Блокировка компьютера 
        /// </summary>
        /// <param name="obj"></param>
        private void BlockedSelectedComputer(object obj) {
            HostModel.LockedComputer(_selectedItem.isLocked);
        }
        public ICommand BlockedComputer
        {
            get
            {
                if (_blockedComputer == null)
                {
                    _blockedComputer = new RelayCommand(BlockedSelectedComputer);
                }

                return _blockedComputer;
            }
        }
        /// <summary>
        /// Кнопка добавления
        /// </summary>
        private Boolean _button_enabled;
        public Boolean ButtonAddEnabled
        {
            get { return _button_enabled; }
            set
            {
                _button_enabled = value; 
                OnPropertyChanged("ButtonAddEnabled"); 
            }
        }
        /// <summary>
        /// Кнопка удаления
        /// </summary>
        private Boolean _buttonRemove_enabled;
        public Boolean ButtonRemoveEnabled
        {
            get { return _buttonRemove_enabled; }
            set
            {
                _buttonRemove_enabled = value;
                OnPropertyChanged("ButtonRemoveEnabled");
            }
        }
        /// <summary>
        /// Выбранный элемент списка
        /// </summary>
        private Computer _selectedItem;
        public Computer SelectedItem
        {
            get {return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                    return;

                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
    }
}
