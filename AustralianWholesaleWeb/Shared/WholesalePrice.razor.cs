using AustralianWholesaleLib.Models;
using Microsoft.AspNetCore.Components;
using Nem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustralianWholesaleWeb.Shared
{
    public partial class WholesalePrice : ComponentBase
    {
        [Inject]
        protected AustralianWholesaleLib.API Lib { get; set; }

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

        private bool ValidRegion => Region != NemRegionId.NONE;
        private decimal kWhPrice;
        private decimal MWhPrice;
        private string kWhDollarString => $"{kWhPrice:c}";
        private string MWhDollarString => $"{MWhPrice:c}";

        private string myStyle = "";

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
            if (RegionId != NemRegionId.NONE)
            {
                var prices = await Lib.CurrentPrice(RegionId);
                kWhPrice = prices.kWh;
                MWhPrice = prices.MWh;

                myStyle = $"background-color: {prices.Colour()};height: 100%;";
            }
            else
            {
                myStyle = $"height: 100%;";
            }
            StateHasChanged();
        }

        private List<Regions> Regions()
        {
            return Lib.Regions();
        }
    }
}
