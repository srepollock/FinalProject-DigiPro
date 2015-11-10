using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    /*
        DFT
        Purpose:
            Object to store all functions for Descrete Fourier Transform calls.
            Created to clear up code, and to pass information through.
    */
    class DFT
    {
        /* This returns an arry of complex numbers. This is not the amplitude of the wave. It must be run through Pythagorus. */
        /*
            DFTFunc
            Purpose:
                Forward DFT for a double array of samples of size n. This 
                converts data in the time domain to the frequency domain. It
                returns a complex number array for usage in inverse DFT 
                funcitons. To properly plot the data returned from this, data
                will have to be run through pythagorus.
            Parameters:
                s:  Signal we are changing
                n:  Size of the signal
        */
        public newComplex[] DFTFunc(double[] s, int n)
        {
            newComplex[] cmplx = new newComplex[n];
            double re; /*real*/
            double im; /*imaginary*/
            for (int f = 0; f < n - 1; f++)
            {
                re = 0;
                im = 0;
                for (int t = 0; t < n - 1; t++)
                {
                    re += s[t] * Math.Cos(2 * Math.PI * t * f / n);
                    im -= s[t] * Math.Sin(2 * Math.PI * t * f / n);
                }
                cmplx[f] = new newComplex(re, im);
            }
            return cmplx;
        }

        /*
            newDFTFunc
            Purpose:
                Forward DFT for a double array of samples of size n. This 
                converts data in the time domain to the frequency domain. 
                This will perform pythagorus to convert the complex array
                directly to usable data for the frequency domain chart.
            Parameters:
                s:  Signal we are changing
                n:  Size of the signal
        */
        public double[] newDFTFunc(double[] s, int n)
        {
            double[] amplitude = new double[n];
            double temp;
            newComplex cmplx;
            double re; /*real*/
            double im; /*imaginary*/
            for (int f = 0; f < n - 1; f++)
            {
                re = 0;
                im = 0;
                for (int t = 0; t < n - 1; t++)
                {
                    re += s[t] * Math.Cos(2 * Math.PI * t * f / n);
                    im -= s[t] * Math.Sin(2 * Math.PI * t * f / n);
                }
                cmplx = new newComplex(re, im);
                temp = (cmplx.getReal() * cmplx.getReal()) + (cmplx.getImaginary() * cmplx.getImaginary());
                temp = Math.Sqrt(temp);
                amplitude[f] = temp; // These are the points we are going to plot.

                // am I squaring i?
            }
            return amplitude;
        }

        /* Preforms the inverse of DFT: this will be displayed to the graph for testing with cosWavCreation */
        /*
            invDFT
            Purpose:
                Inverse Descrete Fourier Transform, used to convert from the
                frequency domain to the time domain.
            Variables:
                A:  Complex array to convert
                n:  Size of the complex array
        */
        public double[] invDFT(newComplex[] A, int n)
        {
            double[] s = new double[n];
            double re; /*real*/
            double im; /*imaginary*/
            for (int t = 0; t < n - 1; t++)
            {
                re = 0;
                im = 0;
                for (int f = 0; f < n - 1; f++)
                {
                    re += A[f].getReal() * Math.Cos(2 * Math.PI * t * f / n);
                    im -= A[f].getImaginary() * Math.Sin(2 * Math.PI * t * f / n);
                }
                s[t] = (re + im) / n;
            }
            return s;
        }
    }
}
