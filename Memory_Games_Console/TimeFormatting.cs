using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public static class TimeFormatting
    {
        public static string FormatTime(double time)
        {
            if (time / 60 >= 1)
            {
                return $"{(int)time / 60} min {Math.Round(time % 60, 2)} s";
            }
            else
            {
                return $"{Math.Round(time, 2)} s";
            }
        }
    }
}
