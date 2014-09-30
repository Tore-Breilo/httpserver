using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Todo: porten bør passe med en web-browser eg. 80, 8080 eller 8888
            TcpListener serversocket = new TcpListener(8888);
            serversocket.Start();
            Console.WriteLine("Hello http server");

            while (true)
            {
                TcpClient connectionSocket = serversocket.AcceptTcpClient();
                EchoService service = new EchoService(connectionSocket);
                Task.Factory.StartNew(() => service.DoIt());

            }
        }
    }
}
