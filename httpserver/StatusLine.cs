using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class StatusLine
    {
        private const string CrLf = "\r\n";
        private string _method="";
        private string _url="";
        private string _version="";

        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public StatusLine(String s)
        {
            string[] ss = s.Split(' ');
            Method = ss[0];
            Url = ss[1];
            Version = ss[2];
        }

        public override string ToString()
        {
            return _version+" "+ _url+" "+_method +CrLf;
        }
    }
}
