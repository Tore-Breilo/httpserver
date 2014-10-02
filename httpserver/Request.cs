using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace httpserver
{
    internal enum methods { GET,POST, DELETE, HEAD}
    

    /// <summary>
    /// A helping class for splitting up a request
    /// </summary>
    internal class Request
    {
        private const byte Cr = 13;
        private const byte Lf = 10;

        public StatusLine RequestLine; //Method_sp_URL_sp_Version
        private List<string> Header; // Name:_sp_Value
        public byte[] EntityBody; //hænger sammen med to headere: Content-Length: & Content-Type:
        private byte[] _resultat;
        //String Requestline

        string ReadLine(int pos){
            string s="";
            int count = pos;
            //læser en linie
            while (this._resultat[count] != Lf)
            {
                s += _resultat[count];
                count++;

            }
            if (_resultat[count] == Lf && _resultat[count -1] == Cr)
            {
                return s;
            }
            return null;

        }

        public Request()
        {
            
        }
        public  void Read(Stream ns)
        {
        
           
	        _resultat=null;

            using (var sr = new MemoryStream())
            {
                ns.CopyTo( sr);
                _resultat=sr.ToArray();               
            }

            int count = 0;
            string linje = this.ReadLine(count);
            count = linje.Count();
           
            do
            {
                if (linje != "" && count < _resultat.Length)
                {
                    Header.Add(linje);
                    count += linje.Count();
                }
            } while (linje != "" && count<_resultat.Length);

        }
              

            //string line = "";
            //// læser requestline
            //RequestLine = sr.ReadLine();

            //// reads headers
            //do
            //{
            //    line = sr.ReadLine();
            //    if (line!="") Header.Add(line);
            //} while (line != "");

            //// read data/EntityBody

            //EntityBody =  sr.re();

        
    }
}

