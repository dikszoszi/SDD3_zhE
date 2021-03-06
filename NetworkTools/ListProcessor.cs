using System.Collections.Generic;
using System.Linq;

namespace NetworkTools
{
    public static class ListProcessor
    {
        public static IEnumerable<PingStats> GetSummarizedStats(IEnumerable<PingStats> originalList)
        {
            if (originalList is null) throw new System.ArgumentNullException(nameof(originalList), "Parametre cannot be null");

            return from pingStat in originalList
                   group pingStat by pingStat.Address into grp
                   select new PingStats
                   {
                       Address = grp.Key,
                       Packets = grp.Sum(ps => ps.Packets),
                       TotalBytes = grp.Sum(ps => ps.TotalBytes),
                       TotalMs = grp.Sum(ps => ps.TotalMs)
                   };
        }
    }
}
