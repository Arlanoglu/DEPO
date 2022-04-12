using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IP_Islemleri
{
    public class IPAddresses
    {
        public static string GetHostName()
        {
            //10.3.21.122
            string ip = "";

            string hostName = Dns.GetHostName();
            var addreses = Dns.GetHostAddresses(hostName);
            foreach (var item in addreses)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = item.ToString();
                }
            }

            return ip;
        }
    }
}
