
using System;

public class NEM_FORECAST_PRICE
{
    public DateTime SETTLEMENTDATE { get; set; }
    public string REGIONID { get; set; }
    public string REGION { get; set; }
    public float RRP { get; set; }
    public float TOTALDEMAND { get; set; }
    public string PERIODTYPE { get; set; }
    public float NETINTERCHANGE { get; set; }
    public float SCHEDULEDGENERATION { get; set; }
    public float SEMISCHEDULEDGENERATION { get; set; }
}
