using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nem
{
    public interface INemService
    {
        NemPrice CurrentPrice(NemRegionId regionId);
        Task<NemPrice> CurrentPriceAsync(NemRegionId regionId);

        List<NemPrice> ForecastedPrices(NemRegionId regionId, TimeScale timeScale = TimeScale.THIRTY_MIN);
        Task<List<NemPrice>> ForecastedPricesAsync(NemRegionId regionId, TimeScale timeScale = TimeScale.THIRTY_MIN);

        DateTime AEMOTime();
    }
}
