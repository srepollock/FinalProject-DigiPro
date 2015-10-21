using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    public class globalChartSelect
    {
        private double chartSelectStart;
        private double chartSelectEnd;

        public globalChartSelect() { chartSelectStart = 0; chartSelectEnd = 0; }

        public double getStart() { return chartSelectStart; }
        public void setStart(double start) { chartSelectStart = start; }
        public double getEnd() { return chartSelectEnd; }
        public void setEnd(double end) { chartSelectEnd = end; }
    }
}
