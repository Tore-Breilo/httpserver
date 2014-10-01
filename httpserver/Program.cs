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
        private static readonly string RootCatalog = "c:/temp";
        public const string CrLf = "\r\n";
        const string Lf = "\n";

        static void Main(string[] args)
        {
            // Todo: porten bør passe med en web-browser eg. 80, 8080 eller 8888

            TcpListener serversocket = new TcpListener(IPAddress.Parse("127.0.0.1"),8888);
            serversocket.Start();
            Console.WriteLine("Hello http server");

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
