using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace FinalProject_v3
{
    class Handle
    {
        public byte[] recordData;

        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEFORMAT
        {
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitPerSample;
            public ushort cbSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEHDR
        {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;
        }
        public delegate void RecordingDelegate(IntPtr deviceHandle, uint message, IntPtr instance, ref WAVEHDR wavehdr, IntPtr reserved2);
        [DllImport("winmm.dll")]
        public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint cWaveHdrSize);
        [DllImport("winmm.dll")]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint Size);
        [DllImport("winmm.dll")]
        public static extern int waveInStart(IntPtr hWaveIn);
        [DllImport("winmm.dll", EntryPoint = "waveInOpen", SetLastError = true)]
        public static extern int waveInOpen(ref IntPtr t, uint id, ref WAVEFORMAT pwfx, IntPtr dwCallback, int dwInstance, int fdwOpen);
        [DllImport("winmm.dll", EntryPoint = "waveInUnprepareHeader", SetLastError = true)]
        public static extern int waveInUnprepareHeader(IntPtr hwi, ref WAVEHDR pwh, uint cbwh);
        [DllImport("winmm.dll", EntryPoint = "waveInStop", SetLastError = true)]
        static extern uint waveInStop(IntPtr hwi);
        [DllImport("winmm.dll", EntryPoint = "waveInClose", SetLastError = true)]
        public static extern uint waveInClose(IntPtr hwnd);
        [DllImport("winmm.dll", EntryPoint = "waveInReset", SetLastError = true)]
        static extern uint waveInReset(IntPtr hwi);
        [DllImport("winmm.dll", EntryPoint = "waveOutOpen", SetLastError = true)]
        public static extern int waveOutOpen(ref IntPtr t, uint id, ref WAVEFORMAT pwfx, IntPtr dwCallback, int dwInstance, int fdwOpen);
        [DllImport("winmm.dll", EntryPoint = "waveOutPrepareHeader", SetLastError = true)]
        public static extern int waveOutPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint Size);
        [DllImport("winmm.dll", EntryPoint = "waveOutWrite", SetLastError = true)]
        public static extern int waveOutWrite(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint Size);
        [DllImport("winmm.dll", EntryPoint = "waveOutUnprepareHeader", SetLastError = true)]
        public static extern int waveOutUnprepareHeader(IntPtr hwi, ref WAVEHDR pwh, uint cbwh);
        [DllImport("winmm.dll", EntryPoint = "waveOutClose", SetLastError = true)]
        public static extern uint waveOutClose(IntPtr hwnd);
        [DllImport("winmm.dll", EntryPoint = "waveOutStart", SetLastError = true)]
        public static extern int waveOutStart(IntPtr hWaveIn);
        [DllImport("winmm.dll", EntryPoint = "waveOutStop", SetLastError = true)]
        static extern uint waveOutStop(IntPtr hwi);
        [DllImport("winmm.dll", EntryPoint = "waveOutReset", SetLastError = true)]
        static extern uint waveOutReset(IntPtr hwi);

        private Handle.RecordingDelegate waveIn;
        private IntPtr handle;
        private IntPtr hWaveOut;
        private uint bufferLength;
        private WAVEFORMAT format;
        private WAVEHDR header;
        private WAVEHDR Outheader;
        private GCHandle headerPin;
        private GCHandle bufferPin;
        private GCHandle savePin;

        private byte[] save;
        private double[] converted;
        private byte[] buffer;

        public wave_file_header getHeader()
        {
            wave_file_header header = new wave_file_header();

            header.init(); // riff, wav, fmd, data
                           // chunkID, format, subchunk1id, subchunk2id
            /*
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitPerSample;
            public ushort cbSize;
            */
            header.SubChunk1Size = 16;
            header.AudioFormat = format.wFormatTag;
            header.NumChannels = format.nChannels;
            header.SampleRate = format.nSamplesPerSec;
            header.ByteRate = format.nAvgBytesPerSec;
            header.BlockAlign = format.nBlockAlign;
            header.BitsPerSample = format.wBitPerSample;
            header.SubChunk2Size = format.cbSize;

            // chunk size based on subchunk2size + 44 (44 for the size of the header)
            header.ChunkSize = header.SubChunk2Size + 44;

            return header;
        }

        public void Record()
        {
            setupWaveIn();
        }

        private void setupBuffer()
        {
            header.lpData = bufferPin.AddrOfPinnedObject();
            header.dwBufferLength = bufferLength;
            header.dwFlags = 0;
            header.dwBytesRecorded = 0;
            header.dwLoops = 0;
            header.dwUser = IntPtr.Zero;
            header.lpNext = IntPtr.Zero;
            header.reserved = IntPtr.Zero;
            headerPin = GCHandle.Alloc(header, GCHandleType.Pinned);

            int i = Handle.waveInPrepareHeader(this.handle, ref header, Convert.ToUInt32(Marshal.SizeOf(header)));
            if (i != 0)
            {
                //Error in waveIn
                return;
            }

            i = Handle.waveInAddBuffer(handle, ref header, Convert.ToUInt32(Marshal.SizeOf(header)));
            if (i != 0)
            {
                //Error om waveInAdd
                return;
            }
        }

        private void setupOutbuffer()
        {
            Outheader.lpData = savePin.AddrOfPinnedObject();
            Outheader.dwBufferLength = (uint)save.Length;
            Outheader.dwFlags = 0x00000004 | 0x00000008;
            Outheader.dwBytesRecorded = 0;
            Outheader.dwLoops = 1;
            Outheader.lpNext = IntPtr.Zero;
            Outheader.reserved = IntPtr.Zero;

            int i = Handle.waveOutPrepareHeader(hWaveOut, ref Outheader, Convert.ToUInt32(Marshal.SizeOf(Outheader)));
            if (i != 0)
                return;

            i = Handle.waveOutWrite(hWaveOut, ref Outheader, Convert.ToUInt32(Marshal.SizeOf(Outheader)));
            if (i != 0)
                return;
        }

        public void reset()
        {
            waveInReset(handle);
            waveOutReset(hWaveOut);
        }

        /*
        public void play(float volume)
        {
            save = adjustVolume(save, volume);
            savePin = GCHandle.Alloc(save, GCHandleType.Pinned);
            hWaveOut = new IntPtr();
            waveIn = this.callbackWaveOut;
            format.wFormatTag = 1; //WAVE_FORMAT_PCM
            format.nChannels = 1;
            format.nSamplesPerSec = 11025;
            format.wBitPerSample = 8;
            format.nBlockAlign = Convert.ToUInt16(format.nChannels * (format.wBitPerSample >> 3));
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
            savePin = GCHandle.Alloc(save, GCHandleType.Pinned);
            format.cbSize = 0;
            //WAVE_MAPPER
            int i = Handle.waveOutOpen(ref hWaveOut, 4294967295, ref format, Marshal.GetFunctionPointerForDelegate(waveIn), 0, 0x0030000);
            if (i != 0)
            {
                //Error
                return;
            }
            setupOutbuffer();
        }
        */

        public void play(double[] data, uint samplespersec)
        {
            int[] intAr = data.Select(x => Convert.ToInt32(Math.Round(x))).ToArray();
            byte[] waveByteData = intAr.Select(x => Convert.ToInt16(x)).SelectMany(x => BitConverter.GetBytes(x)).ToArray();

            save = waveByteData;

            savePin = GCHandle.Alloc(save, GCHandleType.Pinned);

            // change this so that we pass in the header information from the file
            
            hWaveOut = new IntPtr();
            waveIn = this.callbackWaveOut;
            format.wFormatTag = 1; //WAVE_FORMAT_PCM
            format.nChannels = 1;
            format.nSamplesPerSec = samplespersec;
            format.wBitPerSample = 8;
            format.nBlockAlign = Convert.ToUInt16(format.nChannels * (format.wBitPerSample >> 3));
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
            savePin = GCHandle.Alloc(waveByteData, GCHandleType.Pinned);
            format.cbSize = 0;
            //WAVE_MAPPER
            int i = Handle.waveOutOpen(ref hWaveOut, 4294967295, ref format, Marshal.GetFunctionPointerForDelegate(waveIn), 0, 0x0030000);
            if (i != 0)
            {
                //Error
                return;
            }
            setupOutbuffer();
        }

        public void stop_playing()
        {
            Handle.waveOutUnprepareHeader(hWaveOut, ref Outheader, Convert.ToUInt32(Marshal.SizeOf(Outheader)));
            Handle.waveOutClose(hWaveOut);
        }

        private void setupWaveIn()
        {
            handle = new IntPtr();
            waveIn = this.callbackWaveIn;
            format.wFormatTag = 1; //WAVE_FORMAT_PCM
            format.nChannels = 1;
            format.nSamplesPerSec = 11025;
            format.wBitPerSample = 8;
            format.nBlockAlign = Convert.ToUInt16(format.nChannels * (format.wBitPerSample >> 3));
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
            bufferLength = 40000;
            buffer = new byte[bufferLength];
            bufferPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            format.cbSize = 0;
            int i = Handle.waveInOpen(ref handle, 4294967295, ref format, Marshal.GetFunctionPointerForDelegate(waveIn), 0, 0x0030000);
            if (i != 0)
            {
                //Error
                return;
            }
            setupBuffer();
            i = Handle.waveInStart(handle);
            if (i != 0)
            {
                //error
            }
        }

        private void callbackWaveIn(IntPtr deviceHandle, uint message, IntPtr instance, ref WAVEHDR wavehdr, IntPtr reserved2)
        {
            if (message == 0x3BF) //WIM_DATA
            {
                if (save != null)
                {
                    List<byte> temp = save.ToList();
                    temp.AddRange(buffer.ToList());
                    temp.RemoveAll(delegate(byte a) { return a == 0; });
                    save = temp.ToArray();
                }
                else
                    save = buffer;

                savePin = GCHandle.Alloc(save, GCHandleType.Pinned);
                int i = waveInUnprepareHeader(deviceHandle, ref header, Convert.ToUInt32(Marshal.SizeOf(header)));
                if (i != 0) //MMSYSERR_NOERROR
                {
                    //Error
                    return;
                }
                setupBuffer();
            }
        }

        private void callbackWaveOut(IntPtr deviceHandle, uint message, IntPtr instance, ref WAVEHDR wavehdr, IntPtr reserved2)
        {
            if (message == 0x3BF) //WIM_DATA
            {
                List<byte> temp = save.ToList();
                temp.AddRange(buffer.ToList());
                temp.RemoveAll(delegate(byte a) { return a == 0; });
                save = temp.ToArray();
                savePin = GCHandle.Alloc(save, GCHandleType.Pinned);
                int i = waveInUnprepareHeader(deviceHandle, ref header, Convert.ToUInt32(Marshal.SizeOf(header)));
                if (i != 0) //MMSYSERR_NOERROR
                {
                    //Error
                    return;
                }
                setupBuffer();
            }
        }

        public byte[] stop()
        {
            List<byte> temp = buffer.ToList();
            temp.RemoveAll(delegate(byte a) { return a == 0; });
            buffer = temp.ToArray();
            bufferPin.Free();
            bufferPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            waveInStop(handle);
            waveInReset(handle);
            waveInClose(handle);

            return buffer;
        }
    }
}
