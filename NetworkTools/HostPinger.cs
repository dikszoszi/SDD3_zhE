using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetworkTools
{
    public class HostPinger
    {
        private string path;

        public HostPinger(string path)
        {
            this.path = path;
        }

        public PingStats TryOnce(int numTries)
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo("PING.EXE", $"-n {numTries} {this.path}")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();

            string[] allLines = p.StandardOutput.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> replyLines = allLines.Where(line => line.Contains("Reply from"));

            string receivedPackets = allLines[^3]
                .Split(',', StringSplitOptions.TrimEntries)[1]
                .Split(' ', StringSplitOptions.TrimEntries)[^1];

            var bytes = replyLines.Select(line => new
            {
                Bytes = int.Parse(line.Split(' ', StringSplitOptions.TrimEntries).First(splitline => splitline.Contains("bytes=")).Split('=', StringSplitOptions.TrimEntries)[^1])
            });
            var msecs = replyLines.Select(line => new
            {
                Time = int.Parse(line.Split(' ', StringSplitOptions.TrimEntries).First(splitline => splitline.Contains("time=")).Split('=', StringSplitOptions.TrimEntries)[^1].Replace("ms", string.Empty))
            });
            return new PingStats
            {
                Address = this.path + " / " + allLines[^4].Split(' ', StringSplitOptions.TrimEntries)[^1][..^1],
                //Address = this.Path,
                Packets = int.Parse(receivedPackets),
                TotalBytes = bytes.Sum(anonym => anonym.Bytes),
                TotalMs = msecs.Sum(anonym => anonym.Time),
            };
        }
    }
}
