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
            // Todo: porten bør passe med en web-browser ie 80, 8080 eller 8888
            TcpListener serversocket = new TcpListener(8888);
            serversocket.Start();

            while (true)
            {
                TcpClient connectionSocket = serversocket.AcceptTcpClient();
                Console.WriteLine("Hello http server");
                EchoService service = new EchoService(connectionSocket);
               Task.Factory.StartNew(() => service.DoIt());


            }
            
            
            //Console.WriteLine("Hej fra Tore");

            //Stream ns = connectionSocket.GetStream();

            //StreamReader sr = new StreamReader(ns);
            //StreamWriter sw = new StreamWriter(ns);

           
            //ns.Close();
            // også den her
            //Console.WriteLine("Hello http server");
            //Console.WriteLine("Hej fra Tore");

        }
    }
}
