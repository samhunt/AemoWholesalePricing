using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AustralianWholesaleLib.Models;
using Nem;

namespace AustralianWholesaleLib
{
    public class API
    {
        public readonly INemService _nemService;
        public API(INemService nemService)
        {
            _nemService = nemService;
        }

        public List<Regions> Regions()
        {
            return new List<Regions>
            {
                new Regions{ Name = Region.NSW, Id = NemRegionId.NSW1, FullName = "New South Wales" },
                new Regions{ Name = Region.QLD, Id = NemRegionId.QLD1, FullName = "Queensland" },
                new Regions{ Name = Region.SA,  Id = NemRegionId.SA1, FullName = "South Australia" },
                new Regions{ Name = Region.TAS, Id = NemRegionId.TAS1, FullName = "Tasmania" },
                new Regions{ Name = Region.VIC, Id = NemRegionId.VIC1, FullName = "Victoria" },
            };
        }

        public async Task<Price> CurrentPrice(NemRegionId region)
        {
            var result = await _nemService.CurrentPriceAsync(region);

            return new Price
            {
                kWh = result.kWhPrice,
                MWh = result.MWhPrice,
                DateTime = result.DateTime
            };
        }
    }
}
