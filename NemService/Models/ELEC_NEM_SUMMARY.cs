using System;

namespace Nem
{
    public class ELEC_NEM_SUMMARY
    {
        public DateTime SETTLEMENTDATE { get; set; }
        public string REGIONID { get; set; }
        public float PRICE { get; set; }
        public float TOTALDEMAND { get; set; }
        public float NETINTERCHANGE { get; set; }
        public float SCHEDULEDGENERATION { get; set; }
        public float SEMISCHEDULEDGENERATION { get; set; }
        public string INTERCONNECTORFLOWS { get; set; }
    }
}