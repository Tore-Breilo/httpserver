using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Program
    {
        // En lille smule erklæring
        
        public const string CrLf = "\r\n";
        const string Lf = "\n";

        static void Main(string[] args)
        {
            TcpListener serversocket = new TcpListener(IPAddress.Parse("127.0.0.1"),8888);
            serversocket.Start();
            Console.WriteLine("Hello, this is a a very complicated HTTP Server. Access at own risk");

            while (true)
            {
                TcpClient connectionSocket = serversocket.AcceptTcpClient();
                //Console.WriteLine("Hello http server");
                EchoService service = new EchoService(connectionSocket);

                Task.Factory.StartNew(() => service.DoIt());

            }
            
   
        }
    }
}
