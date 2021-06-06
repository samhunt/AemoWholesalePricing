using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nem
{
    public static class Extensions
    {
        public static string GetString(this TimeScale timeScale)
        {
            return timeScale switch
            {
                TimeScale.THIRTY_MIN => "30MIN",
                TimeScale.FIVE_MIN => "5MIN",
                _ => "30MIN",
            };


        }
    }
}
