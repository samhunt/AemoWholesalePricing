using System;
using System.Collections.Generic;
using System.Linq;
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
                kWhPrice = result.kWhPrice,
                MWhPrice = result.MWhPrice,
                DateTime = result.DateTime
            };
        }

        public async Task<ForecastedPrices> ForecastPrices(NemRegionId region)
        {
            var result = await _nemService.ForecastedPricesAsync(region);

            return new ForecastedPrices
            {
                Prices = result.Select(x => new Price
                {
                    DateTime = x.DateTime,
                    DemandMWh = x.Demand,
                    GenerationMWh = x.Generation,
                    kWhPrice = x.kWhPrice,
                    MWhPrice = x.MWhPrice
                }).ToList()
            };
        }
    }
}
