using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingS;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MPing s = new MPing();
            s.AddIp("192.168.46.104");
            s.AddIp("192.168.46.202");
            s.PingStatusChanged += S_PingStatusChanged;
            s.Start();
            Console.Read();
            s.Stop();
            
        }

        private static void S_PingStatusChanged(string ip, bool state)
        {
            Console.WriteLine($"{ip} {state}");
        }
    }
}
