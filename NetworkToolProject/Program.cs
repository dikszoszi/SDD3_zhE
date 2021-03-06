using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetworkTools;

[assembly: CLSCompliant(false)]
namespace NetworkToolProject
{
    internal class Program
    {
        private static void Main()
        {
            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            //HostPinger hp = new HostPinger("www.google.hu");
            //string result = hp.TryOnce(2).ToString();
            MultiHostPinger mhp = new (cts);
            mhp.AddHost("hup.hu");
            mhp.AddHost("bbc.co.uk");
            mhp.AddHost("amazon.com");
            while (!Console.KeyAvailable)
            {
                foreach (PingStats ps in ListProcessor.GetSummarizedStats(AsyncHostPinger.ListCopy)) Console.WriteLine(ps);
                Console.WriteLine("----- ONE CYCLE ENDS -----");
                System.Threading.Thread.Sleep(5000);
            }
            mhp.Cancel();
            cts.Dispose();
        }
    }
}
