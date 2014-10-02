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
        public string Method;
        public string Url;
        public string Version;

        public StatusLine(String s)
        {
            string[] ss;
            ss = s.Split(' ');
            Method = ss[0];
            Url = ss[1];
            Version = ss[2];
        }

    
        
    }
}
