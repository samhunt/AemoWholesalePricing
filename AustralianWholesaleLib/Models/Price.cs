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
        public decimal MWh { get; set; }
        public decimal kWh { get; set; }

        public PriceState State()
        {
            if(MWh <= 0)
            {
                return PriceState.Low;
            }
            if(MWh > 0 && MWh < 20)
            {
                return PriceState.OK;
            }
            if (MWh >= 20 && MWh < 100)
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
