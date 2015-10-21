using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    class newComplex
    {
        private double re, im;
        public newComplex(double ire, double iim) { re = ire; im = iim; }
        public double getReal() { return re; }
        public double getImaginary() { return im; }
    }
}
