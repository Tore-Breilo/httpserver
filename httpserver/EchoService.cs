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
    /// <summary>
    /// En simpel Echo Server
    /// </summary>
    class EchoService

    {
        private const string Sp= " ";
        private const string CrLf = "\r\n";
        private const string Lf = "\n";
        private TcpClient connectionSocket;


        public EchoService(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }
        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing
            // skal der være en autoflush? er der noget der gemmer sig i en buffer?

            string message = sr.ReadLine();
            string answer;
            string reply= "HTTP/1.0" + Sp + "200" + Sp + "OK" + Lf + "Hej Verden" + CrLf;
            
            //læser fra browseren
            while (message != null && message != "")
            {
                Console.WriteLine("Client: " + message);
                answer = message.ToUpper();
                sw.WriteLine(reply);
                message = sr.ReadLine();

            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}
