using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    public class Windowing
    {
        /*
            Triangle
            Purpose:
                This is a funciton that takes in a double array that contains
                signal data. This will create a triangle window based of the
                size of the signal given.
            Parameters:
                wave:   Signal data in a double array to create a window from.
                        The size of the wave data will be used to create a
                        window with.
        */
        public void Triangle(double[] wave)
        {
            int N = wave.Length;
            for(int n = 0; n < N; n++)
            {
                wave[n] = wave[n] * 
                    (1 - Math.Abs((n - ((N - 1) / 2)) / (N / 2)));
            }
        }

        /*
            Triangle
            Purpose:
                This is a funciton that takes in a double array that contains
                signal data. This will create a triangle window based off N.
            Parameters:
                wave:   Signal data in a double array to create a window from.
                        The size of the wave data will be used to create a
                        window with.
                N:      Size of the signal data
        */
        public double[] Triangle(double[] wave, int N)
        {
            double[] temp = wave;
            for (int n = 0; n < N; n++)
            {
                temp[n] = temp[n] *
                    (1 - Math.Abs((n - ((N - 1) / 2)) / (N / 2)));
            }
            return temp;
        }

        /*
            Triangle
            Purpose:
                This is a funciton that takes in a double array that contains
                signal data. This will create a triangle window based of the
                size of the signal given with N.
            Parameters:
                wave:   Signal data in a double array to create a window from.
                        The size of the wave data will be used to create a
                        window with.
                size:      The size of the window we wish to create from the wave
                        data given.
                start:  The starting point in the wave that we wish the window
                        to start on.
        */
        public void Triangle(double[] wave, int size, int start)
        {
            int N = start + size;
            for (int n = start; n < N; n++)
            {
                wave[n] = wave[n] *
                    (1 - Math.Abs((n - ((N - 1) / 2)) / (N / 2)));
            }
        }

        /*
            Rectangle
            Purpose:
                This is a funciton that takes in a double array that contains
                signal data. This will create a Rectangle window based of the
                size of the signal given with N.
            Parameters:
                wave:   Signal data in a double array to create a window from.
                        The size of the wave data will be used to create a
                        window with.
                size:      The size of the window we wish to create from the wave
                        data given.
                start:  The starting point in the wave that we wish the window
                        to start on.
        */
        public void Rectangle(double[] wave, int size, int start)
        {
            int N = start + size;
            for (int n = start; n < N; n++)
            {
                wave[n] = wave[n] * 1;
            }
        }

        /*
            Wench
            Purpose:
                This is a funciton that takes in a double array that contains
                signal data. This will create a Welch window based of the
                size of the signal given with N.
            Parameters:
                wave:   Signal data in a double array to create a window from.
                        The size of the wave data will be used to create a
                        window with.
                size:   The size of the window we wish to create from the wave
                        data given.
                start:  The starting point in the wave that we wish the window
                        to start on.
        */
        public void Welch(double[] wave, int size, int start)
        {
            int N = start + size;
            for (int n = start; n < N; n++)
            {
                wave[n] = wave[n] *
                    (1 - Math.Sqrt((n - ((N - 1) / 2)) / ((N - 1) / 2)));
            }
        }
    }
}
