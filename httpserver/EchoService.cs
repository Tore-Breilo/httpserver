using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace HttpServer
{
    /// <summary>
    ///     En simpel Echo Server
    /// </summary>
    internal class EchoService

    {
        // En lille smule erklæring
        private const string Sp = " ";
        private const string CrLf = "\r\n";
        private const string Lf = "\n";
        private static readonly string RootCatalog = "C:/Temp";
        private readonly TcpClient connectionSocket;


        // Vores connetionsocket
        public EchoService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        public byte[] Filereader(string filename)
        {
            try
            {
                using (var fr = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    // Read the source file into a byte array. 
                    var data = new byte[fr.Length];

                    fr.Read(data, 0, Convert.ToInt32(fr.Length));

                    return data;
                }
            }
            catch (FileNotFoundException ioEx)
            {
                // Hvis filen ikke eksisterer, så bliver der lavet en fejl 404.
                return null;
            }
        }

// slut

        //Vores DO-IT - Metode
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
                        string filename = RootCatalog + messageSplit[1];
                        {
                            // Read the source file into a byte array. 
                            var data = Filereader(filename);
                            string reply = "";

                            if (data == null)
                            {
                                reply = "HTTP/1.0" + Sp + "404" + Sp + "Not Found" + CrLf; // Status line
                                sw.Write(reply);
                            }
                            else
                            {
                                reply = "HTTP/1.0" + Sp + "200" + Sp + "OK" + CrLf + // Status line
                                        "Connection: " + CrLf + //Header
                                        "Date: " + File.GetLastAccessTime(RootCatalog + messageSplit[1]) + CrLf +
                                        //Header Todo datenow
                                        "Server: CaKaTo/0.0.02" + CrLf + //Header
                                        "Last-Modified: " + File.GetLastWriteTime(RootCatalog + messageSplit[1]) + CrLf +
                                        //Header Todo filedate
                                        "Content-Length: " + data.Count() + CrLf + //Header
                                        "Content-Type: txt.html" + CrLf + CrLf; //Header Todo typen skal læses fra fil
                                sw.Write(reply);
                                sw.Flush();
                                ns.Write(data, 0, data.Count()); //data

                                string temp = "";
                                for (int i = 0; i < data.Count(); i++)
                                {
                                    temp += Convert.ToChar(data[i]);
                                }
                                Console.WriteLine("Dette burde være fil-indhold: " + temp);
                            }
                            

                        }
                        Console.WriteLine("" + File.GetLastAccessTime(RootCatalog + messageSplit[1]));
                    }break;
                   
                }
            }
            ns.Close();
            connectionSocket.Close();

        }
    }


}

