using AustralianWholesaleLib.Models;
using Microsoft.AspNetCore.Components;
using Nem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AustralianWholesaleLib.Models.Price;

namespace AustralianWholesaleWeb.Shared
{
    public partial class WholesalePrice : ComponentBase
    {
        [Inject]
        protected AustralianWholesaleLib.API Lib { get; set; }

        private WholesaleForecast _wholesaleForecast;
        private bool ValidRegion => Region != NemRegionId.NONE;
        private decimal kWhPrice;
        private decimal MWhPrice;
        private string kWhDollarString => $"{kWhPrice:c}";
        private string MWhDollarString => $"{MWhPrice:c}";

        private PriceState priceState = PriceState.OK;

        private NemRegionId RegionId = NemRegionId.VIC1;
        private NemRegionId Region
        {
            get { return RegionId; }
            set
            {
                RegionId = value;

                _ = GetLatestPrices();
            }
        }

        public WholesalePrice()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await GetLatestPrices();
            await base.OnInitializedAsync();
        }

        private async Task GetLatestPrices()
        {
            if (_wholesaleForecast != null)
            {
                _wholesaleForecast.RemoveData();
            }

            if (RegionId != NemRegionId.NONE)
            {
                var prices = await Lib.CurrentPrice(RegionId);
                kWhPrice = prices.kWhPrice;
                MWhPrice = prices.MWhPrice;
                priceState = prices.State();
            }

            if (_wholesaleForecast != null)
            {
                await _wholesaleForecast.UpdateData();
            }
            StateHasChanged();
        }


        private string BackgroundGradient => priceState switch
        {
            PriceState.OK => "linear-gradient(to top, #0ba360 0%, #3cba92 100%)",
            PriceState.VeryHigh => "linear-gradient(-60deg, #ff5858 0%, #f09819 100%)",
            PriceState.High => "linear-gradient(120deg, #f6d365 0%, #fda085 100%)",
            PriceState.VeryLow => "linear-gradient(to top, #00c6fb 0%, #005bea 100%)",
            PriceState.Low => "linear-gradient(to top, #00c6fb 0%, #005bea 100%)",
            _ => ""
        };

        private List<Regions> Regions()
        {
            return Lib.Regions();
        }
    }
}
