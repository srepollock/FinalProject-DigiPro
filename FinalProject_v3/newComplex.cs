using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    /*
        newComplex
        Purpose:
            This class is my own created Complex class for creating complex
            numbers. This is used throughout my main program and is easily
            modifiable if need be.
    */
    class newComplex
    {
        /*
            Variables:
                Class variables for usage.
                re: Represents real number
                im: Represents imaginary number
        */
        private double re, im;

        /*
            newComplex
            Purpose:
                <<Constructor>>
                Takes in a real and imaginary number
                for useage.
            Parameters:
                ire:    Real number
                iim:    Imaginary number
        */
        public newComplex(double ire, double iim) { re = ire; im = iim; }

        /*
            getReal
            Purpose:
                Gets the real number from the object.
        */
        public double getReal() { return re; }

        /*
            getImaginary
            Purpose:
                Gets the imaginary number form the object.
        */
        public double getImaginary() { return im; }
    }
}
