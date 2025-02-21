using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComputerBlocker
{
    public class Computer
    {
        private static int _id = 0;
        public int Id { get { return _id; } }
        private IPAddress _ipAddress;
        public IPAddress IpAddress { get { return _ipAddress; } }
        public  bool isLocked { get; set; }
        public Computer(IPAddress ipAddress) {
            _ipAddress = ipAddress;
            ++_id;
            isLocked = false;
        }
    }
}
