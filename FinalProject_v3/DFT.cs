using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private double[] threadAmplitude;

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
            }
            return amplitude;
        }

        /* Preforms the inverse of DFT: this will be displayed to the graph for testing with cosWavCreation */
        /*
            invDFT
            Purpose:
                Inverse Descrete Fourier Transform, used to convert from the
                frequency domain to the time domain.
            Parameters:
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

        /*
            runningDFT
            Purpose:
                This is the DFT function that will be used with threads. This
                will take in the number of threads specified by the user, and
                run DFT for that selection on the whole array. This will then 
                be set to a speccific array for that thread number and used 
                back in the threadDFT funciton to copy the array to the 
                array that will be passed back to the windows form.
            Parameters:
                s:          Wave data that will be processed
                n:          Length of the wave to be processed
                threadNum:  Current thread being run
                maxThreads: Number of threads specified by the user
        */
        private void runningDFT(double[] s, int n, int threadNum, int maxThreads)
        {
            int thNum = threadNum;

            double temp;
            newComplex cmplx;
            double re; //real
            double im; //imaginary

            int startP = ((n / maxThreads) * (thNum - 1)), endP = ((n / maxThreads) * (thNum));
            if(startP < 0)
            {
                startP = 0;
            }
            if (thNum == maxThreads - 1)
            {
                endP = n;
            }

            for (int f = startP; f < endP; f++) // run through first half
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

                threadAmplitude[f] = temp; // These are the points we are going to plot.
            }
        }

        /*
            threadDFTFunc
            Purpose:
                This function takes in the frequency we wish to DFT, the size 
                of the frequency data, and the number of threads selected. It
                will run the number of threads specified by the user. Each will
                copy their DFT'ed data to a personal array that will then be
                copied into an array to be given back to the Form.
            Parameters:
                s:          Wave data to be processed
                n:          Length of the wave data to be processed
                threadNum:  Number of threads to be run
        */
        public double[] threadDFTFunc(double[] s, int n, int threadNum)
        {
            Thread[] tArray = new Thread[threadNum];
            threadAmplitude = new double[n];

            switch (threadNum)
            {
                case 1:
                    tArray[0] = new Thread(() => { runningDFT(s, n, 0, threadNum); });
                    tArray[0].Start();
                    break;
                case 2:
                    tArray[0] = new Thread(() => { runningDFT(s, n, 0, threadNum); });
                    tArray[0].Start();
                    tArray[1] = new Thread(() => { runningDFT(s, n, 1, threadNum); });
                    tArray[1].Start();
                    break;
                case 3:
                    tArray[0] = new Thread(() => { runningDFT(s, n, 0, threadNum); });
                    tArray[0].Start();
                    tArray[1] = new Thread(() => { runningDFT(s, n, 1, threadNum); });
                    tArray[1].Start();
                    tArray[2] = new Thread(() => { runningDFT(s, n, 2, threadNum); });
                    tArray[2].Start();
                    break;
                case 4:
                    tArray[0] = new Thread(() => { runningDFT(s, n, 0, threadNum); });
                    tArray[0].Start();
                    tArray[1] = new Thread(() => { runningDFT(s, n, 1, threadNum); });
                    tArray[1].Start();
                    tArray[2] = new Thread(() => { runningDFT(s, n, 2, threadNum); });
                    tArray[2].Start();
                    tArray[3] = new Thread(() => { runningDFT(s, n, 3, threadNum); });
                    tArray[3].Start();
                    break;
            }

            foreach (Thread th in tArray)
                th.Join();

            return threadAmplitude;
        }
    }
}
