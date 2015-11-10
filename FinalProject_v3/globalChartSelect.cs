using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    /*
        globalChartSelect
        Purpose:
            This class is just a storage for chart selections.
    */
    public class globalChartSelect
    {
        /*
            Variables:
                Class variables for usage.
                chartSelectionStart:    Start of the selection
                chartSelectionEnd:      End of the selection
        */
        private double chartSelectStart;
        private double chartSelectEnd;

        /*
            globalChartSelect
            Purpose:
                <<Constructor>>
                Sets the chartSelectionStart & chartSelectionEnd to 0 for the
                class.
        */
        public globalChartSelect() { chartSelectStart = 0; chartSelectEnd = 0; }

        /*
            getStart
            Purpose:
                Gets the chartSelectionStart value for the call.
        */
        public double getStart() { return chartSelectStart; }

        /*
            setStart
            Purpose:
                Sets the chartSelectionStart value for the object.
        */
        public void setStart(double start) { chartSelectStart = start; }

        /*
            getEnd
            Purpose:
                Gets the chartSelectionEnd value for the call.
        */
        public double getEnd() { return chartSelectEnd; }
       
        /*
            setEnd
            Purpose:
                Sets the chartSelectionEnd value for the object.
        */
        public void setEnd(double end) { chartSelectEnd = end; }
    }
}
