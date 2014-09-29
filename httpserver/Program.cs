using System;
using System.Collections.Generic;
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
            TcpListener serversocket = new TcpListener(65080);
            serversocket.Start();

            TcpClient connectionSocket = serversocket.AcceptTcpClient();
            // nu kan jeg ser CW'en
            // kan du se mig nu?
            Console.WriteLine("Hello http server");
            Console.WriteLine(  "Hej fra Tore");

        }
    }
}
