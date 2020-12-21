using System.Collections.Generic;
using System.Linq;

namespace NetworkTools
{
    public class ListProcessor
    {
        public List<PingStats> GetSummarizedStats(List<PingStats> originalList)
        {
            if (originalList is null) throw new System.ArgumentNullException(nameof(originalList), "Parametre cannot be null");

            return originalList.GroupBy(pingstat => pingstat.Address)
                .Select(grp => new PingStats { Address = grp.Key, Packets = grp.Sum(ps => ps.Packets), TotalBytes = grp.Sum(ps => ps.TotalBytes), TotalMs = grp.Sum(ps => ps.TotalMs) })
                .ToList();
        }
    }
}
