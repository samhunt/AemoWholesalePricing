﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using TimeZoneConverter;

namespace Nem
{
    public class NemService : INemService
    {
        public static string AEMO_TIME_ZONE = "Australia/Melbourne";

        private HttpClient _http;
        public NemService()
        {
            _http = new HttpClient();
        }

        public DateTime AEMOTime() => AEMO_TIME();

        public static DateTime AEMO_TIME()
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(AEMO_TIME_ZONE);

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
        }

        public NemPrice CurrentPrice(NemRegionId regionId)
        {
            return CurrentPriceAsync(regionId).Result;
        }

        public async Task<NemPrice> CurrentPriceAsync(NemRegionId regionId)
        {
            var result = await _http.GetFromJsonAsync<ElecNemSummary>(Path.Combine(Constants.NemApiUrl, Constants.ELEC_NEM_SUMMARY));

            var region = result.ELEC_NEM_SUMMARY.Single(x => x.REGIONID == regionId.ToString());

            return new NemPrice
            {
                DateTime = region.SETTLEMENTDATE,
                Demand = (decimal)region.TOTALDEMAND,
                Generation = (decimal)region.SCHEDULEDGENERATION,
                MWhPrice = (decimal)region.PRICE
            };
        }

        public List<NemPrice> ForecastedPrices(NemRegionId regionId, TimeScale timeScale = TimeScale.THIRTY_MIN)
        {
            return ForecastedPricesAsync(regionId, timeScale).Result;
        }

        public async Task<List<NemPrice>> ForecastedPricesAsync(NemRegionId regionId, TimeScale timeScale = TimeScale.THIRTY_MIN)
        {

            var jsonBody = new { timeScale = new[] { timeScale.GetString() } };
            var path = Path.Combine(Constants.NemApiUrl, Constants.FIVE_MIN);
            var result = await _http.PostAsJsonAsync(path, jsonBody);
            var resultBody = await result.Content.ReadAsStringAsync();
            var dailyPrices = JsonSerializer.Deserialize<DailyPrices>(resultBody);

            return dailyPrices.Prices.Where(x => x.REGIONID == regionId.ToString())
                .Select(x => new NemPrice
                {
                    DateTime = x.SETTLEMENTDATE,
                    Demand = (decimal)x.TOTALDEMAND,
                    Generation = (decimal)x.SCHEDULEDGENERATION,
                    MWhPrice = (decimal)x.RRP
                }).ToList();
        }
    }
}
