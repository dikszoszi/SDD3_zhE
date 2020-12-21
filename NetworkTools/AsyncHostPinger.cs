using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkTools
{
    public class AsyncHostPinger
    {
        private CancellationTokenSource cts;
        private static List<PingStats> pingStatList;

        public AsyncHostPinger(string path, CancellationTokenSource cts)
        {
            this.cts = cts;
            pingStatList = new List<PingStats>();
            Task.Run(() => Pingging(path), this.cts.Token);
        }

        private void Pingging(string path)
        {
            while (true)
            {
                this.cts.Token.ThrowIfCancellationRequested();
                Thread.Sleep(5000);
                HostPinger hp = new HostPinger(path);
                lock (pingStatList)
                {
                    AddToList(hp.TryOnce(2));
                }
            }
        }

        private static void AddToList(PingStats pingStats)
        {
            if (pingStatList.Count < 100) pingStatList.Add(pingStats);
        }

        public static List<PingStats> GetListCopy()
        {
            return pingStatList;
        }

        public void Cancel()
        {
            this.cts.Cancel();
        }
    }
}
