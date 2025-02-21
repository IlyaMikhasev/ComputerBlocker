using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using System.Net.NetworkInformation;

namespace ComputerBlocker
{
    public class Server
    {
        Computer _computer;
        static TcpListener _listener;
        static IPEndPoint _ep;
        public delegate void IncomingMessage(string message);
        public event IncomingMessage onMessage;
        TcpClient listener;
        public Server(Computer computer)
        {
            _computer = computer;
            _ep = new IPEndPoint(long.Parse(computer.IpAddress.ToString()), int.Parse(computer.IpAddress.ToString().Substring(9)));
            _listener = new TcpListener(_ep);
            _listener.Start();
            Task.Run(()=> Listen());
        }
        public void Listen()
        {
            listener = _listener.AcceptTcpClient();
            bool flag = true;
            string epClient = listener.Client.RemoteEndPoint.ToString();
            while (flag)
            {

                byte[] buffer = new byte[4096];
                try
                {
                    listener.Client.Receive(buffer);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                var str = Encoding.Unicode.GetString(buffer);
                if (str != "close")
                {
                    onMessage(str);
                    listener.Client.Send(Encoding.Unicode.GetBytes($"{_computer.isLocked}"));
                }
                else
                    flag = false;
            }
            listener.Close();
        }
        public void Send(string mess)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.Unicode.GetBytes(mess);

            // Begin sending the data to the remote device.  
            listener.Client.Send(byteData,byteData.Length,SocketFlags.None);
        }
        
    }
}
