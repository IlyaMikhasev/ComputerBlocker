using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace ComputerBlocker
{
    internal class Client
    {
        private TcpClient _soket;
        private string _ip_adress;
        public string IP_adress { get { return _ip_adress; } set { _ip_adress = value; } }
        public int Port { get; set; }

        public delegate void IncomingMessage(string message);
        public event IncomingMessage onMessage;
        public Client(string ip_adress, int port)
        {
            _ip_adress = ip_adress;
            Port = port;
            _soket = new TcpClient();
            Task.Factory.StartNew(MessageServer);
        }

        public void MessageServer()
        {
            {
                _soket.Connect(_ip_adress, Port);

                while (true)
                {
                    byte[] buffer = new byte[1024];
                    _soket.Client.Receive(buffer);
                    string mess = Encoding.Unicode.GetString(buffer) + "\r\n";
                    onMessage(mess);
                }
            }

        }
        public void MessageSend(string text)
        {

            try
            {
                if (_soket.Connected)
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(text);
                    _soket.Client.Send(bytes);
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
        public void ServerClose()
        {

            _soket.Close();
        }
    }
}
