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
            p.Dispose();

            string receivedPackets = allLines[^3]
                .Split(',', StringSplitOptions.TrimEntries)[1]
                .Split(' ', StringSplitOptions.TrimEntries)[^1];

            IEnumerable<string> replyLines = allLines.Where(line => line.Contains("Reply from", StringComparison.InvariantCultureIgnoreCase));
            var bytes = replyLines.Select(line => new
            {
                Bytes = int.Parse(line
                    .Split(' ', StringSplitOptions.TrimEntries)
                    .First(splitline => splitline.Contains("bytes=", StringComparison.InvariantCultureIgnoreCase))
                    .Split('=', StringSplitOptions.TrimEntries)[^1]
                  , System.Globalization.NumberFormatInfo.InvariantInfo)
            });
            var msecs = replyLines.Select(line => new
            {
                Time = int.Parse(line
                    .Split(' ', StringSplitOptions.TrimEntries)
                    .First(splitline => splitline.Contains("time=", StringComparison.InvariantCultureIgnoreCase))
                    .Split('=', StringSplitOptions.TrimEntries)[^1]
                    .Replace("ms", string.Empty, StringComparison.InvariantCultureIgnoreCase)
                  , System.Globalization.NumberFormatInfo.InvariantInfo)
            });
            return new PingStats
            {
                Address = this.path + " / " + allLines[^4].Split(' ', StringSplitOptions.TrimEntries)[^1][..^1],
                //Address = this.Path,
                Packets = int.Parse(receivedPackets, System.Globalization.NumberFormatInfo.InvariantInfo),
                TotalBytes = bytes.Sum(anonym => anonym.Bytes),
                TotalMs = msecs.Sum(anonym => anonym.Time),
            };
        }
    }
}
