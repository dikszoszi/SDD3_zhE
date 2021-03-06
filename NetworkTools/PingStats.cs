[assembly: System.CLSCompliant(false)]
namespace NetworkTools
{
    public class PingStats
    {
        public PingStats()
        {
        }

        public string Address { get; set; }
        public int Packets { get; set; }
        public int TotalBytes { get; set; }
        public int TotalMs { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PingStats other &&
                   (Address == other.Address || (this.Address is null && other.Address is null)) &&
                   Packets == other.Packets &&
                   TotalBytes == other.TotalBytes &&
                   TotalMs == other.TotalMs;
        }

        public override int GetHashCode()
        {
            return this.Packets + this.TotalBytes + this.TotalMs;
        }

        public override string ToString()
        {
            return $"Pingged {this.Address}, received {this.Packets} packets, TOTAL: {this.TotalBytes} Bytes in {this.TotalMs}mseconds";
        }
    }
}
