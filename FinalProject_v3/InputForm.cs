using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using System.Media;

namespace FinalProject_v3
{
    public partial class InputForm : Form
    {
        private DFT DFT = new DFT();
        private wave_file_header globalWavHead = new wave_file_header(); // global variable. Used for the read/write of the data
        //private wave_file_header globalSaveWaveHead = new wave_file_header();

        // File
        private string globalFilePath;

        // Wave data
        private double[] globalFreq; // sine wave
        //private newComplex[] globalCmplx; // DFT complex numbers
        private double[] globalAmp; // amplitude of wave

        // Copy and paste selection points
        public globalChartSelect globalChartSelection = new globalChartSelect();
        public globalChartSelect globalHFTChartSelection = new globalChartSelect();
        public globalChartSelect globalCopyPoints = new globalChartSelect();

        // For using mmioStringToFOURCC
        [DllImport("winmm.dll")]
        public static extern int mmioStringToFOURCC([MarshalAs(UnmanagedType.LPStr)] String s, int flags);
        [DllImport("winmm.dll")]
        public static extern int mciSendString(String MciCommand, String MciReturn, int MciReturnLength, int CallBack);

        public InputForm()
        {
            InitializeComponent();
            newFreqBtn.Enabled = false;
            filterAudioToolStripMenuItem.Enabled = false;
        }

        public double[] readingWave(String fileName)
        {
            byte[] byteArray;
            BinaryReader reader = new BinaryReader(File.OpenRead(fileName));
            globalWavHead.clear();
            globalWavHead.ChunkID = reader.ReadInt32();
            globalWavHead.ChunkSize = reader.ReadInt32();
            globalWavHead.Format = reader.ReadInt32();
            globalWavHead.SubChunk1ID = reader.ReadInt32();
            globalWavHead.SubChunk1Size = reader.ReadInt32();
            globalWavHead.AudioFormat = reader.ReadInt16();
            globalWavHead.NumChannels = reader.ReadInt16();
            globalWavHead.SampleRate = reader.ReadInt32();
            globalWavHead.ByteRate = reader.ReadInt32();
            globalWavHead.BlockAlign = reader.ReadInt16();
            globalWavHead.BitsPerSample = reader.ReadInt16();
            globalWavHead.SubChunk2ID = reader.ReadInt32();
            globalWavHead.SubChunk2Size = reader.ReadInt32();
            byteArray = reader.ReadBytes((int)globalWavHead.SubChunk2Size);
            short[] shortAr = new short[globalWavHead.SubChunk2Size / globalWavHead.BlockAlign];
            double[] outputArray;
            for (int i = 0; i < globalWavHead.SubChunk2Size / globalWavHead.BlockAlign; i++)
            { shortAr[i] = BitConverter.ToInt16(byteArray, i * globalWavHead.BlockAlign); }
            outputArray = shortAr.Select(x => (double)(x)).ToArray();
            reader.Close();
            return outputArray;
        }

        public void OpenFile(string fileName)
        {
            globalFilePath = fileName;
            // try to set numericsUpDown to the values of the wave here
            globalFreq = readingWave(globalFilePath);
        //    globalCmplx = DFT.DFTFunc(globalFreq, globalFreq.Length);
        //    globalAmp = amplitudeLength(globalCmplx, globalFreq.Length);
            HFTChart.Series[0].Points.Clear(); // clears the data of the amplitude chart
            freqWaveChart.Series[0].Points.Clear(); // clears the data in the wave form chart
            // Plots the wave data
            plotFreqWaveChart(globalFreq);
            // Plots the data to the DFT Chart
            //    plotHFTWaveChart(globalAmp);
            newFreqBtn.Enabled = true;
        }

        /*
            Saves the the data to the specified file.
        */
        public void SaveFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);
            // initialize new wav head
            // freq, samp
            globalWavHead.initialize(freqUpDown.Value, (decimal)globalFreq.Length);
            fwrite(wr, globalWavHead);
            // convert to int
            int[] intAr = globalFreq.Select(x => Convert.ToInt32(Math.Round(x))).ToArray();
            // convert to bytes
            byte[] waveByteData = intAr.Select(x => Convert.ToInt16(x)).SelectMany(x => BitConverter.GetBytes(x)).ToArray();
            fwrite(wr, waveByteData.Length, waveByteData);
            f.Close();
            globalFilePath = fileName;
        }

        // writes wav header data to file
        public void fwrite(BinaryWriter file, wave_file_header waveHead)
        {
            file.Write(waveHead.ChunkID);
            file.Write(waveHead.ChunkSize);
            file.Write(waveHead.Format);
            file.Write(waveHead.SubChunk1ID);
            file.Write(waveHead.SubChunk1Size);
            file.Write(waveHead.AudioFormat);
            file.Write(waveHead.NumChannels);
            file.Write(waveHead.SampleRate);
            file.Write(waveHead.ByteRate);
            file.Write(waveHead.BlockAlign);
            file.Write(waveHead.BitsPerSample);
            file.Write(waveHead.SubChunk2ID);
            file.Write(waveHead.SubChunk2Size);
        }

        // writes wav data to file
        public void fwrite(BinaryWriter file, int samples, byte[] data)
        {
            // doubling sample size
            for (int i = 0; i < samples; i++)
            { file.Write(data[i]); }
        }

        // Not needed
        /* This is where the amplitude is created. It preforms pythagorus on the  */
        private double[] amplitudeLength(newComplex[] cmplx, int n)
        {
            double[] s = new double[n];
            double cSqr;
            for (int i = 0; i < n - 1; i++)
            {
                /* sqrt of a^2 + b^2 Pythagorus*/
                cSqr = (cmplx[i].getReal() * cmplx[i].getReal()) + (cmplx[i].getImaginary() * cmplx[i].getImaginary());
                cSqr = Math.Sqrt(cSqr);
                s[i] = cSqr; // These are the points we are going to plot.
            }
            return s;
        }

        /* Creates a cosine wave for the tests. */
        private double[] cosWaveCreation(double amp, double freq, int n)
        {
            double[] outputData = new double[n];
            for(int t = 0; t < n; t++)
            { outputData[t] = amp * Math.Cos(2 * Math.PI * t * freq / n); }
            return outputData;
        }

        private void inputButton_Click(object sender, EventArgs e)
        {
            globalWavHead.initialize(freqUpDown.Value, sampUpDown.Value);
            // Get input data to create a frequency from the user
            double amp = (double)ampUpDown.Value;
            double freqData = (double)freqUpDown.Value;
            int samp = (int)sampUpDown.Value;
            // Frequencey plot
            globalFreq = cosWaveCreation(amp, freqData, samp);
            // Perform Half Fourier Transform
            // Get amplitudes of that array
        //    globalAmp = DFT.newDFTFunc(globalFreq, globalFreq.Length);

            plotFreqWaveChart(globalFreq);

            // Plots the data to the DFT Chart
        //    plotHFTWaveChart(globalAmp);
            newFreqBtn.Enabled = true;
        }

        public void plotFreqWaveChart(double[] freq)
        {
            freqWaveChart.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            { freqWaveChart.Series[0].Points.AddXY(m, freq[m]); }
        }

        public void plotFreqWaveChart(int[] freq)
        {
            freqWaveChart.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            { freqWaveChart.Series[0].Points.AddXY(m, freq[m]); }
        }

        /*
            This will be to add frequencies to the chart
                This has to be a frequency with the same sample size
        */
        public void plotFreqWaveChart(double[] freq, double[] newFreq)
        {
            freqWaveChart.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            {
                globalFreq[m] = freq[m] + newFreq[m];
                freqWaveChart.Series[0].Points.AddXY(m, globalFreq[m]);
            }
        }

        public void plotHFTWaveChart()
        {
            int selection = (int)(globalChartSelection.getEnd() - globalChartSelection.getStart());
            if (selection != 0)
                if (selection > (globalFreq.Length / 2))
                    globalAmp = DFT.newDFTFunc(globalFreq, globalFreq.Length / 10);
                else
                    globalAmp = DFT.newDFTFunc(globalFreq, selection);
            else
                globalAmp = DFT.newDFTFunc(globalFreq, globalFreq.Length / 10);   

            HFTChart.Series[0].Points.Clear();
            for (int i = 0; i < globalAmp.Length; i++)
            { HFTChart.Series["HFT"].Points.AddXY(i, globalAmp[i]); }
            filterAudioToolStripMenuItem.Enabled = true;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void HFTChart_Click(object sender, EventArgs e)
        {
            double dataStart = HFTChart.ChartAreas[0].CursorX.SelectionStart;
            double dataEnd = HFTChart.ChartAreas[0].CursorX.SelectionEnd;
            if(dataStart < dataEnd)
            {
                globalHFTChartSelection.setStart(dataStart);
                globalHFTChartSelection.setEnd(dataEnd);
            } else
            {
                globalHFTChartSelection.setStart(dataEnd);
                globalHFTChartSelection.setEnd(dataStart);
            }
        }
        
        private void freqWaveChart_Click(object sender, EventArgs e)
        {
            double dataStart = freqWaveChart.ChartAreas[0].CursorX.SelectionStart;
            double dataEnd = freqWaveChart.ChartAreas[0].CursorX.SelectionEnd;
            if (dataStart < dataEnd)
            {
                globalChartSelection.setStart(dataStart);
                globalChartSelection.setEnd(dataEnd);
            }
            else
            {
                globalChartSelection.setStart(dataEnd);
                globalChartSelection.setEnd(dataStart);
            }
        }

        public double[] FreqWaveChart_Copy()
        {
            double[] copyArray = new double[(int)(globalChartSelection.getEnd() - globalChartSelection.getStart())];
            for (int i = 0; i < (int)(globalChartSelection.getEnd() - globalChartSelection.getStart()); i++)
            { copyArray[i] = globalFreq[(int)globalChartSelection.getStart() + i]; }
            // set the copy points
            globalCopyPoints.setStart(globalChartSelection.getStart());
            globalCopyPoints.setEnd(globalChartSelection.getEnd());
            
            return copyArray;
        }

        public void FreqWaveChart_Paste(IDataObject copied)
        {
            DataObject retrievedData = (DataObject)Clipboard.GetDataObject();
            if (retrievedData.GetDataPresent(typeof(double[])))
            {
                double[] copyData = retrievedData.GetData(typeof(double[])) as double[];
                double[] temp = new double[globalFreq.Length + copyData.Length];
                int selectionSize = copyData.Length;
                for (int i = 0, j = 0; i < temp.Length; i++, j++)
                {
                    if (i == globalChartSelection.getStart())
                    {
                        // write the data from the copied into the temp
                        for (int l = 0; l < copyData.Length; l++, i++)
                        {
                            temp[i] = copyData[l];
                        }
                    }
                    else
                    {
                        // write the data into the temp
                        temp[i] = globalFreq[j];
                    }
                }
                globalFreq = temp;
                plotFreqWaveChart(globalFreq);
                plotHFTWaveChart();
            }
        }

        public double[] FreqWaveChart_Cut()
        {
            double[] copyArray = new double[(int)(globalChartSelection.getEnd() - globalChartSelection.getStart())];
            copyArray = FreqWaveChart_Copy();
            // delete the copy points
            double[] temp = new double[globalFreq.Length - copyArray.Length];
            for(int i = 0, l = 0; i < globalFreq.Length; i++, l++)
            {
                if(i == globalChartSelection.getStart())
                {
                    for(int j = 0; j < globalChartSelection.getEnd(); i++, j++) { }
                }
                else
                {
                    temp[l] = globalFreq[i];
                }
            }
            globalFreq = temp;
            plotFreqWaveChart(globalFreq);
            plotHFTWaveChart();

            return copyArray;
        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void recButton_Click(object sender, EventArgs e)
        {

        }

        private void stpRecBtn_Click(object sender, EventArgs e)
        {

        }

        private void recordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void stopRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zoomBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turns off chart zooming
            freqWaveChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            HFTChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            selectToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turns on chart zooming
            freqWaveChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            HFTChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            selectToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = true;
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private newComplex[] creationOfLowpassFilter(double[] frequencySize)
        {
            int N = frequencySize.Length;
            newComplex[] outComplex = new newComplex[N];
            double start = globalHFTChartSelection.getStart();

            // create a complex numbers for the selected size, otherwise it is complex zero
            for(int i = 0; i < (N / 2); i++)
            { 
                if(N % 2 != 0)
                {
                    outComplex[N / 2] = new newComplex(0, 0);
                }
                if(i < start)
                {
                    outComplex[i] = new newComplex(1, 1);
                    outComplex[N - i - 1] = new newComplex(1, 1);
                } else
                {
                    outComplex[i] = new newComplex(0, 0);
                    outComplex[N - i - 1] = new newComplex(0, 0);
                }
            }
            return outComplex;
        }

        private newComplex[] creationOfHighpassFilter(double[] frequencySize)
        {
            int N = frequencySize.Length;
            newComplex[] outComplex = new newComplex[N];
            double start = globalHFTChartSelection.getStart();

            // create a complex numbers for the selected size, otherwise it is complex zero
            for (int i = 0; i < (N / 2); i++)
            {
                if (i > start)
                {
                    outComplex[i] = new newComplex(1, 1);
                    outComplex[N - i - 1] = new newComplex(1, 1);
                }
                else
                {
                    outComplex[i] = new newComplex(0, 0);
                    outComplex[N - i - 1] = new newComplex(0, 0);
                }
            }
            if (N % 2 != 0) outComplex[N / 2] = new newComplex(0, 0);

            return outComplex;
        }

        private void convolve(double[] convolutionData, double[] orgSignal)
        {
            int N = orgSignal.Length, WN = convolutionData.Length;
            double[] newSignal = new double[N];
            
            for(int n = 0; n < N; n++)
            {
                double temp = 0;
                for(int wn = 0; wn < WN; wn++)
                {
                    if ((n + wn) < (N - 1)) // if we are less than the frequency data size
                        temp += convolutionData[wn] * orgSignal[n + wn];
                    else
                       temp += 0;
                }
                newSignal[n] = temp;
            }

            globalFreq = newSignal; // setup the new signal
        }

        private void filterAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is where I will filter
            // get the selection of the frequency to filter from the audio file

            /*
                Select the filter
                    in freq view
                        low pass
                            only the low pases
                        high pass
                            only the high passes
                        band pass
                            only the seleection
                    design filter based on pass
                        1/0's
                            invDFT 1/0's
                            convolve the return of the invDFT across original time signal
                                train
            */

            /*
                Cannot select more than the niquist limit
                cannot select more than one point
            */

            newComplex[] filter = creationOfLowpassFilter(globalAmp);

            convolve(DFT.invDFT(filter, filter.Length), globalFreq);

            plotFreqWaveChart(globalFreq);
            plotHFTWaveChart();
        }

        private void highPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newComplex[] filter = creationOfHighpassFilter(globalAmp);

            convolve(DFT.invDFT(filter, filter.Length), globalFreq);

            plotFreqWaveChart(globalFreq);
            plotHFTWaveChart();
        }

        /*
            With these two functions, I want to plot only the selection I have displayed in the
            frequency chart.
        */
        private void plotAmplitudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotHFTWaveChart();
        }

        private void plotFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotFreqWaveChart(globalFreq);
        }

        private void newFreqBtn_Click(object sender, EventArgs e)
        {
            double[] newFreqWave = cosWaveCreation((int)ampUpDown.Value, (double)newFreqUpDown.Value, globalFreq.Length);
            plotFreqWaveChart(globalFreq, newFreqWave);
            plotHFTWaveChart();
        }
    }
}
