using System;
using System.Collections;
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
    internal class EchoService

    {
        private const string Sp = " ";
        private const string CrLf = "\r\n";
        private const string Lf = "\n";

        private static readonly string RootCatalog = "c:/temp";
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
            sw.AutoFlush = true; // automatisk "flusher"

            string message = sr.ReadLine();
            Console.WriteLine(message);
            if (message != null && message != "")
            {
                string[] messageSplit = message.Split(' ');
                switch (messageSplit[0].ToUpper())
                {
                    case "GET":
                    {
                        

                        try
                        {
                            using (FileStream fs = new FileStream(RootCatalog+messageSplit[1],
                                FileMode.Open, FileAccess.Read))
                            {
                                // Read the source file into a byte array. 
                                byte[] data = new byte[fs.Length];
                                fs.Read(data,0, Convert.ToInt32(fs.Length));
                                string reply = "HTTP/1.0" + Sp + "200" + Sp + "OK" + CrLf +
                                               //"For some reason you requested the file:" + messageSplit[1] + CrLf + CrLf;
                                               "Connection: close" + CrLf +
                                               "Date: Tue, 09 Aug 2011 15:44:04 GMT" + CrLf +
                                               "Server: KasperTorian/0.0.01" + CrLf +
                                               "Last-Modified: Tue, 09 Aug 2011 15:11:03 GMT" + CrLf +
                                               "Content-Length: " + Convert.ToString(fs.Length) + CrLf +
                                               "Content-Type: text/html" + CrLf + CrLf;
                                               
                                sw.Write(reply);
                                sw.Write(data);
                            Console.WriteLine("Dette burde være fil-indhold: /n"+data);    
                            }
                        }
                        catch (FileNotFoundException ioEx)
                        {
                            Console.WriteLine(ioEx.Message);
                        }
                    }


                       

                        break;
                }
            }
        ns.Close();
         connectionSocket.Close();
        }


       

    }
}


