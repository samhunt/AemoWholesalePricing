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

        public string Colour()
        {
            if(MWh <= 0)
            {
                return _blue;
            }
            if(MWh > 0 && MWh < 20)
            {
                return _green;
            }
            if (MWh >= 20 && MWh < 100)
            {
                return _orange;
            }


            return _red;
        }

        private string _red = "#d9534f";
        private string _orange = "#f0ad4e";
        private string _green = "#5cb85c";
        private string _blue = "#5bc0de";
    }
}
