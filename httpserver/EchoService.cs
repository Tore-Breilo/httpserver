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
                            using (FileStream fr = new FileStream(RootCatalog+messageSplit[1],
                                FileMode.Open, FileAccess.Read))
                            {
                                // Read the source file into a byte array. 
                                byte[] data = new byte[fr.Length];

                                fr.Read(data,0, Convert.ToInt32(fr.Length));
                                // todo bør smide en file not found (4xx) hvis ej fundet
                                
                                string reply = "HTTP/1.0" + Sp + "200" + Sp + "OK" + CrLf +     // Status line
                                               "Connection: close" + CrLf +                     //Header
                                               "Date: " + File.GetLastAccessTime(RootCatalog+messageSplit[1]) + CrLf +   //Header Todo datenow
                                               "Server: CaKaTo/0.0.02" + CrLf +           //Header
                                               "Last-Modified: Tue, 09 Aug 2011 15:11:03 GMT" + CrLf + //Header Todo filedate
                                               "Content-Length: " + Convert.ToString(fr.Length) + CrLf + //Header
                                               "Content-Type: text/html" + CrLf + CrLf;         //Header Todo typen skal læses fra fil
                                               
                                sw.Write(reply);
                                sw.Flush();
                                ns.Write(data, 0 , data.Count());                       //data
                                string temp = "";
                                for (int i = 0; i < data.Count(); i++)
                                {
                                    temp+=Convert.ToChar(data[i]);
                                }
                            Console.WriteLine("Dette burde være fil-indhold: "+temp);    
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


