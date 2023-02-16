using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitilabAPICore
{
    public class Events
    {
        public static Action<string> Message;

        public static void Requestheaders(ref RestRequest request)
        {
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Accept", "*/*");
            //request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Connection", "keep-alive");
        }
    }
}
