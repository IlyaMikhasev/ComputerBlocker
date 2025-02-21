using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerBlocker
{
    public class HomeModel
    {
        public static void Unlock(LockedWindow window) {
            window.Unlock();
        }
        public static void Lock(LockedWindow window)
        {
            window.ShowDialog();
        }
    }
}
