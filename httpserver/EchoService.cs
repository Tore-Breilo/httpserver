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
          this.connectionSocket = connectionSocket;
        }

        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;                        // automatisk "flusher"
            
            string message = sr.ReadLine();
            Console.WriteLine(message);
            string[] messageSplit = message.Split(' ');
            
            string reply= "HTTP/1.0" + Sp + "200" + Sp + "OK" + CrLf + CrLf + messageSplit[1];


            sw.WriteLine(reply);

            
            
	  

            //læser fra browseren
            /*while (message != null && message != "")
            {
                Console.WriteLine("Client: " + message);
               
                sw.WriteLine(reply);
                message = sr.ReadLine();
            }*/
            }

            ns.Close();
            connectionSocket.Close();
        }
    }
}
