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
            Triangle window.
            This will rewrite the wave passed in.
        */
        public void Triangle(double[] wave)
        {
            int N = wave.Length;
            for(int n = 0; n < N; n++)
            {
                wave[n] = wave[n] * (1 - Math.Abs((n - ((N - 1) / 2)) / (N / 2)));
            }
        }
    }
}
