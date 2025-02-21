using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerBlocker
{
    public class HomeWindowModel : ObservableObject
    {
        LockedWindow form;
        private DateTime _timeLocked;
        public bool lockedTimeNow { get; set; }
        public bool unlockedTime { get; set; }
        public DateTime timeLocked { get { return _timeLocked; }
            set {
                if (lockedTimeNow)
                {
                    _timeLocked = DateTime.Now;
                }
                else 
                    _timeLocked = value;
            } }
        private DateTime? _timeUnlocked;
        public DateTime? timeUnlocked
        {
            get { return _timeUnlocked; }
            set
            {
                if (unlockedTime)
                {
                    _timeUnlocked = value;
                }
                else
                    _timeUnlocked = null;
            }
        }


        public HomeWindowModel() { 
        }
        public string Password { get { return SettingsModel.Password; } }
        private ICommand _lockWin;
        public ICommand LockWin
        {
            get
            {
                if (_lockWin == null)
                {
                    _lockWin = new RelayCommand(
                       p => {
                           form = new LockedWindow();
                           form.ShowDialog(); }); 
                }

                return _lockWin;
            }
        }
        private void StartBlock()
        {
            if (unlockedTime)
            {
                form = new LockedWindow();
                form.ShowDialog();
                new Thread(() => { Unlock(); }).Start();
            }
            else
            {
                form = new LockedWindow();
                form.ShowDialog();
            }
        }
        private void Unlock() {
            while (DateTime.Now != _timeUnlocked) {
                Task.Delay(1000);
            }
            form.Unlock();
        }
    }
}
