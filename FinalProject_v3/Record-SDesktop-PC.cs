using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace FinalProject_v3
{
    public partial class Record
    {
        uint INP_BUFFER_SIZE = 16384;
        const int CALLBACK_FUNCTION = 0x0030000;
        const int CALLBACK_WINDOW = 0x0010000;
        public bool endRecording = false;
        public static WaveHeader pWaveHead1, pWaveHead2;

        public delegate void AudioRecordingDelegate(IntPtr deviceHandle, uint message, IntPtr instance, ref WaveHeader waveHeader, IntPtr reserved2);

        [DllImport("winmm.dll")]
        public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WaveHeader lpWaveHdr, uint Size);
        [DllImport("winmm.dll")]
        public static extern int WaveInPrepareHeader(IntPtr hWaveIn, ref WaveHeader lpWaveHeader, uint Size);
        [DllImport("winmm.dll")]
        public static extern int waveInStart(IntPtr hWaveIn);
        [DllImport("winmm.dll")]
        public static extern int waveInOpen(ref IntPtr t, uint id, ref WaveFormatEx pwfx, IntPtr dwcallBack, int dwInstance, int fdwOpen);
        [DllImport("winmm.dll")]
        public static extern int waveInUnprepareHeader(IntPtr hwi, ref WaveHeader pwh, uint cbwh);
        [DllImport("winmm.dll", EntryPoint = "waveInStop", SetLastError = true)]
        static extern uint waveInStop(IntPtr hwi);
        [DllImport("winmm.dll", EntryPoint = "waveInReset", SetLastError = true)]
        static extern uint waveInReset(IntPtr hwi);
        [DllImport("winmm.dll", EntryPoint = "waveInClose", SetLastError = true)]
        static extern uint waveInClose(IntPtr hwi);

        public static IntPtr handle;
        private GCHandle bufferPin;
        private static uint sampleLength = 0;
        private byte[] samples = new byte[0];
        private AudioRecordingDelegate waveIn;
        private byte[] buffer;
        private uint bufferLength;

        public int setupBuffer()
        {
            pWaveHead1 = new WaveHeader();
            pWaveHead1.lpData = bufferPin.AddrOfPinnedObject();
            pWaveHead1.dwBufferLength = INP_BUFFER_SIZE;
            pWaveHead1.dwBytesRecorded = 0;
            pWaveHead1.dwUser = IntPtr.Zero;
            pWaveHead1.dwFlags = 0;
            pWaveHead1.dwLoops = 1;
            pWaveHead1.lpNext = IntPtr.Zero;
            pWaveHead1.reserved = (System.IntPtr)null;

            int i = WaveInPrepareHeader(handle, ref pWaveHead1, Convert.ToUInt32(Marshal.SizeOf(pWaveHead1)));
            if (i != 0)
            {
                //Error: waveInPrepare
                return -2;
            }
            i = waveInAddBuffer(handle, ref pWaveHead1, Convert.ToUInt32(Marshal.SizeOf(pWaveHead1)));
            if (i != 0)
            {
                //Error: waveInAddrBuffer
                return -3;
            }
            return 0;
        }

        public int setupWaveIn()
        {
            waveIn = this.callbackWaveIn;
            handle = new IntPtr();
            WaveFormatEx format = new WaveFormatEx();
            format.wFormatTag = 1;
            format.nchannels = 1;
            format.nSamplesPerSec = 11025;
            format.wBitsPerSample = 8;
            format.nBlockAlign = 1;
            format.nAvgBytesPerSec = 11025;
            bufferLength = 16384;
            format.cbSize = 0;

            buffer = new byte[bufferLength];
            bufferPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            int i = waveInOpen(ref handle, 4294967295, ref format, Marshal.GetFunctionPointerForDelegate(waveIn), 0, CALLBACK_FUNCTION);
            if (i != 0)
            {
                //Error: waveInOpen
                return -1;
            }
            int j = setupBuffer();
            if (j != 0)
            {
                return j;
            }
            i = waveInStart(handle);
            if (i != 0)
            {
                //Error: waveInStart + i
                return i;
            }
            return 0;
        }

        public void callbackWaveIn(IntPtr deviceHandle, uint message, IntPtr instance, ref WaveHeader waveheader, IntPtr reserved2)
        {
            int i = 0;

            if (message == 0x3BF)
            {

            }
            else if(message == 958)
            {
                return;
            }
            else if (message == 0x3C0)
            {
                if (waveheader.dwBytesRecorded > 0)
                {
                    byte[] temp = new byte[sampleLength + waveheader.dwBytesRecorded];
                    Array.Copy(samples, temp, sampleLength);

                    Array.Copy(buffer, 0, temp, sampleLength, waveheader.dwBytesRecorded);
                    sampleLength += waveheader.dwBytesRecorded;
                    samples = temp;

                    i = waveInUnprepareHeader(deviceHandle, ref waveheader, Convert.ToUInt32(Marshal.SizeOf(waveheader)));
                    if (i != 0)
                    {
                        //Error: waveInUnprepareHeader + i
                    }
                    setupBuffer();
                }
                if (endRecording)
                    return;
            }
            else
            {
                // Shouldn't get here
                endRecording = true;
            }
        }

        public void stopRecording()
        {
            uint ii = waveInStop(handle);
            endRecording = true;
            System.Threading.Thread.Sleep(200);
            ii = waveInReset(handle);
            ii = waveInClose(handle);
        }

        public byte[] getSamples()
        {
            return samples;
        }
    }
}
