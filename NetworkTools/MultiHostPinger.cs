using System.Collections.Generic;
using System.Threading;

namespace NetworkTools
{
    public class MultiHostPinger
    {
        private CancellationTokenSource cts;
        private List<AsyncHostPinger> ahpList;

        public MultiHostPinger(CancellationTokenSource cts)
        {
            this.cts = cts;
            this.ahpList = new List<AsyncHostPinger>();
        }

        public void AddHost(string address)
        {
            this.ahpList.Add(new AsyncHostPinger(address, this.cts));
        }

        public void Cancel()
        {
            foreach (AsyncHostPinger ahp in this.ahpList) ahp.Cancel();
        }
    }
}
