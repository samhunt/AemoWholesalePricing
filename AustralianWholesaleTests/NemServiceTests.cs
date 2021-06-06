using FluentAssertions;
using Nem;
using System.Threading.Tasks;
using Xunit;

namespace AustralianWholesaleTests
{
    public class NemServiceTests
    {
        [Fact]
        public async Task CurrentPriceAsync_Success()
        {
            var nemService = new NemService();

            var currentPrice = await nemService.CurrentPriceAsync(NemRegionId.VIC1);

            currentPrice.Should().NotBeNull();
        }

        [Fact]
        public async Task ForecastedPricesAsync_Success()
        {
            var nemService = new NemService();

            var forecastedPrices = await nemService.ForecastedPricesAsync(NemRegionId.VIC1);

            forecastedPrices.Should().NotBeNull();
        }

        [Fact]
        public void CurrentPrice_Success()
        {
            var nemService = new NemService();

            var currentPrice = nemService.CurrentPrice(NemRegionId.VIC1);

            currentPrice.Should().NotBeNull();
        }

        [Fact]
        public void ForecastedPrices_Success()
        {
            var nemService = new NemService();

            var forecastedPrices = nemService.ForecastedPrices(NemRegionId.VIC1);

            forecastedPrices.Should().NotBeNull();
        }
    }
}
