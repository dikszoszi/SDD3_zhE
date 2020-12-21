using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetworkTools;

namespace NetworkToolProject
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            //HostPinger hp = new HostPinger("www.google.hu");
            //string result = hp.TryOnce(2).ToString();
            ListProcessor lp = new ListProcessor();
            MultiHostPinger mhp = new MultiHostPinger(cts);
            mhp.AddHost("hup.hu");
            mhp.AddHost("bbc.co.uk");
            mhp.AddHost("amazon.com");
            while (!Console.KeyAvailable)
            {
                foreach (PingStats ps in lp.GetSummarizedStats(AsyncHostPinger.GetListCopy())) Console.WriteLine(ps);
                Console.WriteLine("----- ONE CYCLE ENDS -----");
                System.Threading.Thread.Sleep(5000);
            }
            mhp.Cancel();
        }
    }
}
