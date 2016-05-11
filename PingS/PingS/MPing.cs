using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingS
{
    public class MPing
    {
        public delegate void PingStatusChangedHandler(string ip, bool state);

        public event PingStatusChangedHandler PingStatusChanged;
        // Invoke the Changed event; called whenever list changes
        private bool PPing(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
               
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
        public bool PingHost(string nameOrAddress)
        {
            return PPing(nameOrAddress);
        }
        List<string> mLstIp = new List<string>();
        private const int TIMEDELAY = 5000;
        public bool Status
        {
            get; set;
        }
        public void AddIp(string pS)
        {
            mLstIp.Add(pS);
        }
        
        public void Start()
        {
            Status = true;
            Thread _P = new Thread(()=> {
                
                while(Status)
                {
                    Thread.Sleep(TIMEDELAY);
                    foreach (string _s in mLstIp)
                    {
                        
                        Thread _t = new Thread(() =>
                        {
                            bool value = this.PingHost(_s);
                            if (PingStatusChanged != null)
                            {
                                PingStatusChanged(_s, value);
                            }
                        }
                        );
                        _t.Start();
                        
                    }
                    
                }
             }
            );
            _P.Start();
        }
        public void Stop()
        {
            Status = false;
            Thread.Sleep(TIMEDELAY);
        }
    }
}
