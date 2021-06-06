using System;
using System.Diagnostics;

namespace Nem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NemPrice
    {
        public DateTime DateTime { get; set; }
        public decimal MWhPrice { get; set; }
        public decimal kWhPrice => MWhPrice / 1000M;
        public decimal Demand { get; set; }
        public decimal Generation { get; set; }

        private string DebuggerDisplay => $"{DateTime:dd/MM/yyyy HH:mm}, Price: ${MWhPrice}, Demand: {Demand} MW, Generation: {Generation} MW";
    }
}