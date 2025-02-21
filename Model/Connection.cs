using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerBlocker.Model
{
    public class Connection
    {
        Computer _computer ;
        Client _client ;
        Server _server ;
        public Connection(Computer computer) {
            _computer = computer;
        }
        public void ServerStart() {
            _server = new Server(_computer);
            _server.Listen();
        }
        public void ClientStart() {
            _client.MessageServer();
        }
        

    }
    
}
