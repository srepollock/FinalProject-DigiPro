﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        /*
            DFT
            Used to call DFT class funcitons in the program.
        */
        private DFT DFT = new DFT();
        /*
            threads
            This is the number of threads specified by the user to run the DFT
            function with.
            Defualted to 1.
        */
        private int threads = 1;
        /*
            globalWavHead
            Class object for the .wav file header. This is used for the
            current file int the window.
        */
        private wave_file_header globalWavHead = new wave_file_header();

        // File
        /*
            globalFilePath
            Class variable for the .wav file path. This is set after a save
            or after an opening of a file.
        */
        private string globalFilePath;

        // Wave data
        /*
            globalFreq
            Class variable for the frequency data. This is the array that
            stores all the data used for the signal.
        */
        private double[] globalFreq; // sine wave

        /*
            globalAmp
            Class variable for the amplitude data. This is used for the 
            amplitude data to be used in the chart.
        */
        private double[] globalAmp; // amplitude of wave

        /*
			handler
			Class object to handle the audio recording and playing for the 
			file.
        */
        Recording handler = new Recording();

        // Copy and paste selection points
        /*
            globalChartSelection
            Class object for the time domain selection. This stores the start 
            and end selection points on the time domain chart as selected.
        */
        public globalChartSelect globalChartSelection = new globalChartSelect();

        /*
            globalHFTChartSelection
            Class object for the frequency selection. This stores the start
            and end selection points on the frequency domain chart as selected.
        */
        public globalChartSelect globalHFTChartSelection = new globalChartSelect();

        /*
            globalCopyPoints
            Class object for the selected copy points on the chart. This stores
            the start and end selection points to copy on the time domain.
        */
        public globalChartSelect globalCopyPoints = new globalChartSelect();

        /*
            globalWindowedSelection
            Class object. This object is used to store the selected window 
            points so that if the user selects a new point on the time domain
            graph, they are still able to properly filter the windowed data.
            This variable is only set when the user plots to the frequency 
            domain.
        */
        public globalChartSelect globalWindowedSelection = new globalChartSelect();

        /*
            globalWindowing
            Class object for a windowing algorithm. This will allow one to 
            window the selection for processing. Example being filtering. If
            the user would like to remove just a frequency from a certain point
            in the signal, they may select a portion to filter.
        */
        public Windowing globalWindowing = new Windowing();

        // For using mmioStringToFOURCC
        [DllImport("winmm.dll")]
        public static extern int mmioStringToFOURCC([MarshalAs(UnmanagedType.LPStr)] String s, int flags);
        [DllImport("winmm.dll")]
        public static extern int mciSendString(String MciCommand, String MciReturn, int MciReturnLength, int CallBack);

        public InputForm()
        {
            InitializeComponent();
            globalWavHead.initialize((uint)sampUpDown.Value);
            filterAudioToolStripMenuItem.Enabled = false; // Cannot use until we have plotted the frequency domain chart
            triangleWindowToolStripMenuItem.Enabled = false; // Cannot use until we have a selection
            rectangleWindowToolStripMenuItem.Enabled = false; // Cannot use until we have a selection
            welchWindowToolStripMenuItem.Enabled = false;  // Cannot use until we have a selection
            stopRec.Enabled = false;
            playButton.Enabled = false;
        }

        /*
            readingWave
            Purpose:
                Reads a .wav file's information. This sets up the 
                globalWavHead object. Then after reading the file data, the
                program continues to read the rest of the data as information
                to plot to the time domain chart.
            Parameters:
                fileName:   File path for where to find the file to open on the
                            system.
        */
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
            globalWavHead.AudioFormat = reader.ReadUInt16();
            globalWavHead.NumChannels = reader.ReadUInt16();
            globalWavHead.SampleRate = reader.ReadUInt32();
            globalWavHead.ByteRate = reader.ReadUInt32();
            globalWavHead.BlockAlign = reader.ReadUInt16();
            globalWavHead.BitsPerSample = reader.ReadUInt16();
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

        /*
            OpenFile
            Purpose:
                This function is called outside of the class to open a file
                in the current classes window. This in turn calls the 
                readingWave function after getting the fileName.
            Parameters:
                fileName:  File path for where to find the file to open on the
                           system. 
        */
        public void OpenFile(string fileName)
        {
            globalFilePath = fileName; 
            this.Text = fileName; // Sets the text of the form to the file name
            globalFreq = readingWave(globalFilePath);
            sampUpDown.Value = globalWavHead.SampleRate; // we know what the sample rate of the file is already
            lengthOfData.Value = globalWavHead.SubChunk2Size;
            HFTChart.Series[0].Points.Clear(); // clears the data of the amplitude chart
            freqWaveChart.Series[0].Points.Clear(); // clears the data in the wave form chart
            // Plots the wave data
            plotFreqWaveChart(globalFreq);
            playButton.Enabled = true;
        }

        /*
            SaveFile
            Purpose:
                For external calls. This function makes internal calls on the 
                data to save to the specified file name.
            Parameters:
                fileName:   File path to save the data to.
        */
        public void SaveFile(string fileName)
        {
            this.Text = fileName; // Sets the text of the form to the file name
            FileStream f = new FileStream(fileName, FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);
            // initialize new wav head
            // freq, samp

            //globalWavHead.updateSubChunk2(globalFreq.Length);
            //globalWavHead.SubChunk2Size = globalFreq.Length;
            //globalWavHead.ChunkSize = globalWavHead.SubChunk2Size + 44;
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
        /*
            fwrite
            Purpose:
                Writes the information to the file path specified. This will
                write the globalWavHead data to the file.
            Parameters:
                file:       File name to write data to.
                waveHead:   Wave header to write the data to.
        */
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
        /*
            fwrite
            Purpose:
                Writes data to the file path specified with the information 
                passed into the second parameter. This call writes the
                data to the file in a byte array.
            Parameters:
                file:   File path to write the data to
                data:   Data to write to the file.
        */
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

        /*
            cosWaveCreation
            Purpose:
                Creates a cosine wave from the inserted data. This function 
                takes amplitued, frequency and size of the wave we wish to
                initially create. This is the background of creating a new
                file. Without this, we cannot add frequencies and cannot
                start creating a file.
            Parameters:
                amp:    Amplitude of the signal
                freq:   Frequency of the signal
                n:      Number of signal samples
        */
        private double[] cosWaveCreation(double amp, double freq, int n)
        {
            double[] outputData = new double[n];
            for(int t = 0; t < n; t++)
            { outputData[t] = amp * Math.Cos(2 * Math.PI * t * freq / n); }
            return outputData;
        }

        /*
			clearFreqButton_Click
			Purpose:
				This method will clear the current freq domain. It sets the
				size of the globalFreq variable to 1 with no value set inside.
        */
        private void clearFreqButton_Click(object sender, EventArgs e)
        {
            globalFreq = new double[1];
            plotFreqWaveChart(globalFreq);
            lengthOfData.Value = globalFreq.Length;
            freqWaveChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        /*
            !!--All plots take the longest time. This is noticable the most
            time when called on large arrays.--!!
        */

        /*
            plotFreqWaveChart
            Purpose:
                Takes a double array to then plot the data to the time domain
                chart.
            Parameters:
                freq:   Data for the function to plot to the time domain chart
        */
        public void plotFreqWaveChart(double[] freq)
        {
            freqWaveChart.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            { freqWaveChart.Series[0].Points.AddXY(m, freq[m]); }
            lengthOfData.Value = freq.Length;
            freqWaveChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        /*
            plotFreqWaveChart
            Purpose:
                Takes an int array to then plot the data to the time domain
                chart.
            Parameters:
                freq:   Data for the function to plot to the time domain.
        */
        public void plotFreqWaveChart(int[] freq)
        {
            freqWaveChart.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            { freqWaveChart.Series[0].Points.AddXY(m, freq[m]); }
            lengthOfData.Value = freq.Length;
            freqWaveChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        /*
            This will be to add frequencies to the chart
                This has to be a frequency with the same sample size
        */
        /*
            plotFreqWaveChart
            Purpose:
                Adds together the current frequency data with a new frequency
                data. This will create a complex wave, a signal with multiple
                frequencies.
            Parameters:
                freq:       Base signal.
                newFreq:    New frequency to add to the base signal.
        */
        public void plotFreqWaveChart(double[] freq, double[] newFreq)
        {
            freqWaveChart.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            {
                globalFreq[m] = freq[m] + newFreq[m];
                freqWaveChart.Series[0].Points.AddXY(m, globalFreq[m]);
            }
            lengthOfData.Value = freq.Length;
            freqWaveChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        /*
            This will remove the selection from the globalFreq wave, then insert a convolved window of data.
            Will still need to call plot to insert the data to the chart after this function.
        */
        /*
            updateGlobalFreq
            Purpose:
                This function is meant to update the globalFrequency after
                filtering the data. This is meant to take in the data that
                has been filtered, and then add it to the globalFreq array to
                be used. This data will still have to be plotted after 
                completion.
            Parameters:
                filteredWindow: This is the array of the filtered window data.
        */        
        public void updateGlobalFreq(double[] filteredWindow)
        {
            // Use windowed selection
            int start = (int)globalWindowedSelection.getStart();
            double[] temp = new double[globalFreq.Length + filteredWindow.Length];
            for (int GF = 0; GF < globalFreq.Length; GF++)
            {
                if (GF == start)
                {
                    for (int CW = 0; CW < filteredWindow.Length; CW++, GF++)
                    {
                        temp[GF] = filteredWindow[CW];
                    }
                }
                else
                {
                    temp[GF] = globalFreq[GF];
                }
            }
            globalFreq = temp;
            lengthOfData.Value = globalFreq.Length * 2;
            this.Text += "*";
            freqWaveChart.ChartAreas[0].AxisX.Minimum = 0;
        }
        

        /*
            plotHFTWaveChart
            Purpose:
                Plots the frequency domain chart based on the seleciton of the
                user on the time domain.                
        */
        public void plotHFTWaveChart()
        {
            int selection = (int)(globalChartSelection.getEnd() - globalChartSelection.getStart());
            int start = (int)(globalChartSelection.getStart());
            // Save the points for the windowed data
                // This is incase the user selects a new point.
            globalWindowedSelection.setStart(globalChartSelection.getStart());
            globalWindowedSelection.setEnd(globalChartSelection.getEnd());
            double[] copiedFreq = globalFreq;
            globalWindowing.Triangle(copiedFreq, selection, start); // Default
            globalAmp = DFT.threadDFTFunc(copiedFreq, selection, threads);
            HFTChart.Series[0].Points.Clear();
            for (int i = 0; i < globalAmp.Length; i++)
            { HFTChart.Series["DFT"].Points.AddXY(i, globalAmp[i]); }
            filterAudioToolStripMenuItem.Enabled = true;
            HFTChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        /*
            plotHFTWaveChart
            Purpose:
                Plots the frequency domain chart based on the seleciton of the
                user on the time domain. This will check to see if the
                selection from the user is greater than half of the maximum 
                sample size, and if so will minimize the amount of data the 
                function will plot.  
            Parameters:
                              
        */
        public void plotHFTWaveChart(String windowType)
        {
            int selection = (int)(globalChartSelection.getEnd() - globalChartSelection.getStart());
            int start = (int)(globalChartSelection.getStart());
            double[] copiedFreq = globalFreq;
            // Save the points for the windowed data
            // This is incase the user selects a new point.
            globalWindowedSelection.setStart(globalChartSelection.getStart());
            globalWindowedSelection.setEnd(globalChartSelection.getEnd());
            if(String.Compare(windowType, "Triangle") == 0)
            {
                globalWindowing.Triangle(copiedFreq, selection, start);
            }
            else if(String.Compare(windowType, "Rectangle") == 0)
            {
                globalWindowing.Rectangle(copiedFreq, selection, start);
            }
            else if(String.Compare(windowType, "Welch") == 0)
            {
                globalWindowing.Welch(copiedFreq, selection, start);
            }
            //globalAmp = DFT.newDFTFunc(copiedFreq, selection);
            globalAmp = DFT.threadDFTFunc(globalFreq, selection, threads);
            HFTChart.Series[0].Points.Clear();
            for (int i = 0; i < globalAmp.Length; i++)
            { HFTChart.Series["DFT"].Points.AddXY(i, globalAmp[i]); }
            filterAudioToolStripMenuItem.Enabled = true;
            HFTChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        /*
			hzToolStripMenuItem_Click
			Purpose:
				Sets the sample rate of the data to be used as playing;
        */
        private void hzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sampUpDown.Value = 22050;
            globalWavHead.updateSampleRate((uint)sampUpDown.Value);
            this.Text += "*";
        }

		/*
			hzToolStripMenuItem1_Click
			Purpose:
				Sets the sample rate of the data to be used as playing;
        */
        private void hzToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sampUpDown.Value = 44100;
            globalWavHead.updateSampleRate((uint)sampUpDown.Value);
            this.Text += "*";
        }

        /*
			hzToolStripMenuItem2_Click
			Purpose:
				Sets the sample rate of the data to be used as playing;
        */
        private void hzToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            sampUpDown.Value = 11025;
            globalWavHead.updateSampleRate((uint)sampUpDown.Value);
            this.Text += "*";
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

        /*
            HFTChart_Click
            Purpose:
                Whenever the frequency domain chart is clicked, the values will
                be saved to the globalHFTCharSelection object.
        */
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
        
        /*
            freqWaveChart_Click
            Purpose:
                Whenever the time domain chart is clicked, the values will
                be saved to the globalChartSelection object.
        */
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
            if((globalChartSelection.getEnd() - globalChartSelection.getStart()) > 0) // If we have selected more that 1
            {
                triangleWindowToolStripMenuItem.Enabled = true;
                rectangleWindowToolStripMenuItem.Enabled = true;
                welchWindowToolStripMenuItem.Enabled = true;
            }
            else // Set the button as disabled until we have something selected
            {
                triangleWindowToolStripMenuItem.Enabled = false;
                rectangleWindowToolStripMenuItem.Enabled = false;
                welchWindowToolStripMenuItem.Enabled = false;
            }
        }

        /*
            FreqWaveChart_Copy
            Purpose:
                External function call. This is the function that is called 
                outside the class. When called, the function will take the
                start and end values specified in the globalChartSelection
                for the time domain, and take all values in the specified
               range, create an array and return it.
        */
        public double[] FreqWaveChart_Copy()
        {
            if (globalChartSelection.getStart() == -1)
                return globalFreq;
            double[] copyArray = new double[(int)(globalChartSelection.getEnd() - globalChartSelection.getStart())];
            for (int i = 0; i < (int)(globalChartSelection.getEnd() - globalChartSelection.getStart()); i++)
            { copyArray[i] = globalFreq[(int)globalChartSelection.getStart() + i]; }
            // set the copy points
            globalCopyPoints.setStart(globalChartSelection.getStart());
            globalCopyPoints.setEnd(globalChartSelection.getEnd());
            return copyArray;
        }

        /*
            FreqWaveChart_Paste
            Purpose:
                Takes in an IDataObject to copy to the globalFreq data. This
                IDataObject contains data from the clipboard to add to the 
                signal.
            Parameters:
                copied: Data taken from the clipboard to add the the
                globalFreq array.
        */
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
                globalWavHead.updateSubChunk2(globalFreq.Length * globalWavHead.BlockAlign);
                lengthOfData.Value = globalFreq.Length;
                this.Text += "*";
            }
        }

        /*
            FreqWaveChart_Cut
            Purpose:
                This function is essentially copy, as it returns an array with
                copied data from the globalFreq array. However, instead of
                just copyingthe data from the array, this function will also
                remove the data from the array.
        */
        public double[] FreqWaveChart_Cut()
        {
            if (globalChartSelection.getStart() == -1)
            {
                return globalFreq;
            }
            List<double> copiedData = new List<double>(FreqWaveChart_Copy()); // this is the copied data
            List<double> list = new List<double>(globalFreq);
            // go to the position of such to remove
            list.RemoveRange((int)globalChartSelection.getStart(), (int)(globalChartSelection.getEnd() - globalChartSelection.getStart()));
            globalFreq = list.ToArray();
            plotFreqWaveChart(globalFreq); // this takes the longest time
            this.Text += "*";
            globalWavHead.updateSubChunk2(globalFreq.Length * globalWavHead.BlockAlign);
            lengthOfData.Value = globalFreq.Length;
            return copiedData.ToArray();
        }

        public void FreqWaveChart_Del()
        {
            List<double> list = new List<double>(globalFreq);
            // go to the position of such to remove
            list.RemoveRange((int)globalChartSelection.getStart(), (int)(globalChartSelection.getEnd() - globalChartSelection.getStart()));
            globalFreq = list.ToArray();
            plotFreqWaveChart(globalFreq); // this takes the longest time
            this.Text += "*";
            globalWavHead.updateSubChunk2(globalFreq.Length * globalWavHead.BlockAlign);
            lengthOfData.Value = globalFreq.Length;
        }

        /*
            selectToolStripMenuButton_Click
            Purpose:
                On menu button click, will turn on selection for the charts,
                this will turn off zoom selection.
        */
        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turns off chart zooming
            freqWaveChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            HFTChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            selectToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
        }

        /*
            zoomToolStripMenuButton_Click
            Purpose:
                On menu button click, will turn on zoom for the charts,
                this will turn off data selection.
        */
        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turns on chart zooming
            freqWaveChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            HFTChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            selectToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = true;
        }

        /*
            InputForm_Load
            Purpose:
                This is a function to call on the form creation.
        */
        private void InputForm_Load(object sender, EventArgs e)
        {

        }
        
        /*
            creationOfLowpassFilter
            Purpose:
                This will create an array of values for a lowpass filter.
                This function uses the frequency chart start selection, stored
                in globalHFTChartSelection object. The function will then
                create an array of complex 1's and 0's. The complex one's are
                everything from 0 - start (selection). That is, at the Nyquist 
                limit, half of the signal array, the data is mirrored, and 
                therefore the complex 1's and 0's are then mirrored.
            Parameters:
                frequencySize:  Size of the frequency data array that we are
                                preforming the lowpass filter on.
        */
        private newComplex[] creationOfLowpassFilter(double[] frequencySize)
        {
            int N = frequencySize.Length; // amplitude displayed on DFT chart
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

        /*
            creationOfHighPassFilter
            Purpose:
                This will create an array of values for a highpass filter.
                This function uses the frequency chart start selection, stored
                in globalHFTChartSelection object. The function will then
                create an array of complex 1's and 0's. The complex one's are
                everything from start (selection) - n/2. That is, at the 
                Nyquist limit, half of the signal array, the data is mirrored,
                and therefore the complex 1's and 0's are then mirrored.
            Parameters:
                frequencySize:  Size of the frequency data array that we are
                                preforming the highpass filter on.
        */
        /* // not Implemented yet
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
        */

        /*
            convolve
            Purpose:
                After we have inverse DFTed the data taken from the filter
                creation, we now convolve the data with that of the signal we
                are filtering. This is to be thought of as a train. As the 
                train travels, along the track, the carts are times by
                that of the data signal. When we run out of cars to data, we
                move the train by 1, and start again. Thus the train is run
                through all the data. If we to not have an equal number of
                cars to data, we must choose to either pad with 0's or no.
            Parameters:
                convolutionData:    Data gathered after performing inverse DFT
                                    on the filter to gain weights.
                orgSignal:          Original signal we plan to filter on.
        */
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

        /*
            filterAudiotoolStripMenuItem
            Purpose:
                Button to call the functions to create the filter. After one
                has selected the filter point on the frequency domain chart,
                we can now create a lowpass filter. This function will call
                creationOfLowpassFilter, convolve and inverse DFT.
        */
        private void filterAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is where I will filter
            // get the selection of the frequency to filter from the audio file
            this.Text += " - Filtering...";
            newComplex[] filter = creationOfLowpassFilter(globalAmp);
            convolve(DFT.invDFT(filter, filter.Length), globalFreq);
            plotFreqWaveChart(globalFreq);
            plotHFTWaveChart();
            this.Text = globalFilePath;
            this.Text += "*";
        }

        /*
            plotAmplitudeToolStripMenuItem_Click
            Purpose:
                Button to call to plot triangle window.
        */
        private void triangleWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotHFTWaveChart("Triangle");
        }

        /*
            rectangleWindowToolStripMenuItem_Click
            Purpose:
                Button to call to plot rectangle window.
        */
        private void rectangleWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotHFTWaveChart("Rectangle");
        }

        /*
            hammingWindowToolStripMenuItem_Click
            Purpose:
                Button to call to plot hamming window.
        */
        private void welchWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotHFTWaveChart("Welch");
        }

        /*
            plotFrequencytoolStripMenuItem_Click
            Purpose:
                Button to call plotFreqWaveChart for the time domain.
        */
        private void plotFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotFreqWaveChart(globalFreq);
        }

        /*
			threads1MenuButton_Click
			Purpose:
				This method sets the thread count for the DFT function to 1.
        */
        private void threads1MenuButton_Click(object sender, EventArgs e)
        {
            threads = 1;
            threads2MenuButton.Checked = false;
            threads3MenuButton.Checked = false;
            threads4MenuButton.Checked = false;
        }

        /*
			threads2MenuButton_Click
			Purpose:
				This method sets the thread count for the DFT function to 2.
        */
        private void threads2MenuButton_Click(object sender, EventArgs e)
        {
            threads = 2;
            threads1MenuButton.Checked = false;
            threads3MenuButton.Checked = false;
            threads4MenuButton.Checked = false;
        }

        /*
			threads3MenuButton_Click
			Purpose:
				This method sets the thread count for the DFT function to 3.
        */
        private void threads3MenuButton_Click(object sender, EventArgs e)
        {
            threads = 3;
            threads1MenuButton.Checked = false;
            threads2MenuButton.Checked = false;
            threads4MenuButton.Checked = false;
        }

        /*
			threads4MenuButton_Click
			Purpose:
				This method sets the thread count for the DFT function to 4.
        */
        private void threads4MenuButton_Click(object sender, EventArgs e)
        {
            threads = 4;
            threads1MenuButton.Checked = false;
            threads2MenuButton.Checked = false;
            threads3MenuButton.Checked = false;
        }

        /*
			recButton_Click
			Purpose:
				This will allow the user to begin recording audio from the 
				standard audio input device connected to their computer. 
				This will then call the handler.Record() function that will
				record the audio through DLL imported C functions.
        */
        private void recButton_Click(object sender, EventArgs e)
        {
            recButton.Enabled = false;
            stopRec.Enabled = true;
            playButton.Enabled = false;
            handler.Record();
        }

        /*
			stopRec_Click
			Purpose:
				This will stop the recording by calling the handler.stopRec()
				method. This sends a stop message through imported C functions.
				This will also get the wave_header of that recorded and set
				it to the globalWavHead.
        */
        private void stopRec_Click(object sender, EventArgs e)
        {
            byte[] temp = handler.stop();
            // get the header and set it to the global wav header
            // this should just update the header information
            wave_file_header tempHeader = handler.getHeader(); // we want to just get the size
                                                               // of the data recorded
                                                               // I want 1 byte to 1 double
            globalWavHead.updateSampleRate(tempHeader.SampleRate);

            short[] shortAr = new short[temp.Length / globalWavHead.BlockAlign];
            for (int i = 0; i < (temp.Length / globalWavHead.BlockAlign); i++)
            { shortAr[i] = BitConverter.ToInt16(temp, i * globalWavHead.BlockAlign); }
            globalFreq = shortAr.Select(x => (double)(x)).ToArray();
            globalWavHead.updateSubChunk2(globalFreq.Length * globalWavHead.BlockAlign);
            plotFreqWaveChart(globalFreq);
            recButton.Enabled = true;
            stopRec.Enabled = false;
            playButton.Enabled = true;
        }

        /*
			playButton_Click
			Purpose:
				This will allow the user to play the current signal displayed.
				This method passes in the data we wish to play, as well as 
				playing at the current sample rate defined by the user.
        */
        private void playButton_Click(object sender, EventArgs e)
        {
        	// Plays at the current sample rate defined by user control
            handler.play(globalFreq, globalWavHead);
            recButton.Enabled = true;
            stopRec.Enabled = false;
        }
    }
}
