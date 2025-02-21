using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ComputerBlocker
{
    public class HostModel
    {
        private static ObservableCollection<Computer> _dataComputers = new ObservableCollection<Computer>();

       
        public static ObservableCollection<Computer> GetComputers()
        {            
            return _dataComputers;
        }

        public static void AddComputer(Computer computer) {
            _dataComputers.Add(computer);
        }
        public static void RemoveComputer(Computer computer)
        {
            if(_dataComputers.Count==0)
                return;
            _dataComputers.Remove(computer);
        }
        public static bool equelsComputerToData(IPAddress address) {
            foreach (var comp in _dataComputers)
            {
                if (comp.IpAddress.Equals(address))
                    return true;
                else return false;
            }
            return false;
        }
        public static void LockedComputer(bool isLocked) {
            if (isLocked)
            {
                return;
            }
            else {
                
                LockedWindow lockedWindow = new LockedWindow();
                lockedWindow.ShowDialog();
                
            }

        }

    }
}
