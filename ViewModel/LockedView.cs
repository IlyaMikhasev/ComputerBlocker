using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerBlocker
{
    public class LockedView :ObservableObject
    {
        public string password { get { return SettingsModel.Password; }}
        public LockedView()
        {
        }
    }
}
