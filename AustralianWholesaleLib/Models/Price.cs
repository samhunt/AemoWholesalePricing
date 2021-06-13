using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustralianWholesaleLib.Models
{
    public class Price
    {
        public DateTime DateTime { get; set; }
        public decimal MWhPrice { get; set; }
        public decimal kWhPrice { get; set; }

        public decimal GenerationMWh { get; set; }

        public decimal DemandMWh { get; set; }

        public PriceState State()
        {
            if(MWhPrice <= 0)
            {
                return PriceState.Low;
            }
            if(MWhPrice > 0 && MWhPrice < 20)
            {
                return PriceState.OK;
            }
            if (MWhPrice >= 20 && MWhPrice < 100)
            {
                return PriceState.High;
            }


            return PriceState.VeryHigh;
        }

        public enum PriceState
        {
            VeryHigh,
            High,
            OK,
            Low,
            VeryLow
        }
    }
}
