using Microsoft.AspNetCore.Components;
using Nem;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;
using ChartJs.Blazor.Common.Time;
using ChartJs.Blazor;
using ChartJs.Blazor.Common;

namespace AustralianWholesaleWeb.Shared
{
    public partial class WholesaleForecast : ComponentBase
    {
        [Inject]
        protected AustralianWholesaleLib.API Lib { get; set; }
        [Parameter]
        public NemRegionId Region { get; set; }

        private LineConfig _config;
        private Chart _chart;

        private string YAxis_MWh = "MWh";
        private string YAxis_DollarkWh = "$/kWh";

        protected override async Task OnInitializedAsync()
        {
            _config = new LineConfig
            {
                Options = new LineOptions
                {
                    Responsive = true,
                    Scales = new Scales
                    {
                        XAxes = new List<CartesianAxis>
                        {
                            new TimeAxis
                            {
                                Distribution = TimeDistribution.Linear,
                                Ticks = new TimeTicks
                                {
                                    Source = TickSource.Data
                                },

                                Time = new TimeOptions
                                {
                                    TooltipFormat = "DD/MM/yyyy HH:mm",
                                },
                            }
                        },
                        YAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ID = YAxis_MWh,
                                Position = Position.Right,
                                Display = AxisDisplay.True,
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = YAxis_MWh
                                }

                            },

                            new LinearCartesianAxis
                            {
                                ID = YAxis_DollarkWh,
                                Position = Position.Left,
                                Display = AxisDisplay.True,
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = YAxis_DollarkWh
                                }

                            },

                        }
                    }
                }
            };

            await UpdateData();
        }

        public async Task UpdateData()
        {

            var forecasedPrices = await Lib.ForecastPrices(Region);

            var data = forecasedPrices.Prices.OrderBy(x => x.DateTime).ToList();
            data.RemoveAll(x => x.DateTime <= NemService.AEMO_TIME().AddHours(-6)
                                || x.DateTime >= NemService.AEMO_TIME().AddHours(16));
            RemoveData();
            _config.Data.Datasets.Add(new LineDataset<TimePoint>(data.Select(x => new TimePoint(x.DateTime, (double)x.kWhPrice)).ToList()) { 
                Label = "$/kWh", 
                BorderColor = "white", 
                YAxisId = YAxis_DollarkWh,
                PointRadius = 0,
                PointHoverRadius = 10,
                PointHitRadius = 10,
            });
            _config.Data.Datasets.Add(new LineDataset<TimePoint>(data.Select(x => new TimePoint(x.DateTime, (double)x.GenerationMWh)).ToList()) { 
                Label = "Generation MWh", 
                BorderColor = "green", 
                YAxisId = YAxis_MWh,
                PointRadius = 0,
                PointHoverRadius = 10,
                PointHitRadius = 10,
            });
            _config.Data.Datasets.Add(new LineDataset<TimePoint>(data.Select(x => new TimePoint(x.DateTime, (double)x.DemandMWh)).ToList()) { 
                Label = "Demand MWh", 
                BorderColor = "blue", 
                YAxisId = YAxis_MWh, 
                PointRadius = 0,
                PointHoverRadius = 10,
                PointHitRadius = 10,
            });

            await _chart.Update();
            StateHasChanged();
        }

        public void RemoveData()
        {
            if (_config.Data.Datasets.Count == 0)
                return;

            IList<string> labels = _config.Data.Labels;
            if (labels.Count > 0)
                labels.RemoveAt(labels.Count - 1);

            foreach (IDataset dataset in _config.Data.Datasets)
            {
                if (dataset is LineDataset<TimePoint> pointDataset &&
                    pointDataset.Count > 0)
                {
                    pointDataset.RemoveAt(pointDataset.Count - 1);
                }
            }
            _config.Data.Datasets.Clear();

            _chart.Update();
        }
    }
}
