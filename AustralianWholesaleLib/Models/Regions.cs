using Nem;

namespace AustralianWholesaleLib.Models
{
    public class Regions
    {
        public Region Name { get; set; }
        public NemRegionId Id { get; set; }
        public string FullName { get; set; }
    }


    public enum Region
    {
        NSW,
        SA,
        TAS,
        VIC,
        QLD
    };
}