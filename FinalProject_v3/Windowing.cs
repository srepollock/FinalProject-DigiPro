using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_v3
{
    class Windowing
    {
        /*
            Triangle windowing function. This is the only windowing done in the project.
            
            Return: the new data to display after windowing

            w(n) = 1 - abs((n-((N-1)/2))/((L/2))
        */
        public double[] triangle(double[] freq, int N)
        {
            double[] temp = freq;

            for(int n = 0; n < N; n++)
            {
                temp[n] = temp[n] * (1 - Math.Abs((n - ((N - 1) / 2) / (N / 2))));
            }

            return temp;
        }
    }
}
