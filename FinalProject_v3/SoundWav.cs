using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject_v3
{
    public partial class SoundWav : Form
    {
        public SoundWav()
        {
            InitializeComponent();
        }

        private void newInputWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checks if there is already a child form of this type open
            foreach (Form form in Application.OpenForms)
            {
                if(form.GetType() == typeof(InputForm))
                {
                    form.Activate();
                    return;
                }
            }

            InputForm newMDIChild = new InputForm();
            // Sets the Parent form of the Child window.
            newMDIChild.MdiParent = this;
            // Displays the new form
            newMDIChild.Show();
        }

        private void newSoundWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checks if there is already a child form of this type open
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(GraphForm))
                {
                    form.Activate();
                    return;
                }
            }

            GraphForm newMDIChild = new GraphForm();
            // Sets the Parent form of the Child window.
            newMDIChild.MdiParent = this;
            // Displays the new form
            newMDIChild.Show();
        }

        private void closeInputWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Determines active child
            Form activeChild = this.ActiveMdiChild;
            
            // If there is an active child, determine it's type
            if(activeChild is InputForm)
            {
                // Closes window
                activeChild.Close();
            }
        }

        private void cascadeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void horizontallyTileWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void verticallyTileWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void closeSoundWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Determines active child
            Form activeChild = this.ActiveMdiChild;

            // If there is an active child, determine it's type
            if (activeChild is InputForm)
            {
                // Closes window
                activeChild.Close();
            }
        }

        private void newHalfFourierTransformGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checks if there is already a child form of this type open
            foreach (Form form in Application.OpenForms)
            {
                if(form.GetType() == typeof(InputForm))
                {
                    form.Activate();
                    return;
                }
            }
            InputForm newInputForm = new InputForm();
            // Sets the Parent form of the Child window.
            newInputForm.MdiParent = this;
            // Displays the new form
            newInputForm.Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Asks the user if they really want to quit the application
            if (MessageBox.Show("Did you save your work?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm newMDIChild = new InputForm();
            // Sets the Parent form of the Child window.
            newMDIChild.MdiParent = this;
            // Displays the new form
            newMDIChild.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openWavFile.ShowDialog(); // I want to open this to the child window in the file
            if (result == DialogResult.OK) // checks if the result returned true
            {
                
                Form openForm = this.ActiveMdiChild;
                if (openForm != null)
                {
                    if (openForm.GetType() == typeof(InputForm))
                    {
                        InputForm openToChild = (InputForm)this.ActiveMdiChild;
                        openToChild.OpenFile(openWavFile.FileName);
                        return;
                    }
                }
                
                // if there is no child window of InputForm, open a new one
                InputForm childWnd = new InputForm();
                childWnd.OpenFile(openWavFile.FileName); // opens the file of the specified type in the child window
                childWnd.MdiParent = this;
                childWnd.Show(); // enter the child window
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveWavFile.ShowDialog();
            if(result == DialogResult.OK && this.ActiveMdiChild == null)
            {
                return;
            }
            else if (result == DialogResult.OK)
            {
                if (this.ActiveMdiChild.GetType() == typeof(InputForm))
                {
                    InputForm inputForm = (InputForm)this.ActiveMdiChild;
                    inputForm.SaveFile(saveWavFile.FileName);
                    return;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) { saveWavFile.ShowDialog(); }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void openHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileTestForm openHelpText = new openFileTestForm();
            String filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Help.txt";
            openHelpText.OpenFile(filePath);
            openHelpText.MdiParent = this;
            openHelpText.Show();
        }

        private void closeHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            if (activeChild is openFileTestForm)
            { activeChild.Close(); }
        }

        private void yesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileTestForm openTest = new openFileTestForm();
            openTest.OpenFile("yes");
            openTest.MdiParent = this;
            openTest.Show();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            if(activeChild == null) { return; } // if no child, break
            if(activeChild.GetType() == typeof(InputForm))
            {
                InputForm chartInput = (InputForm)activeChild; // become an InputForm
                double[] array = chartInput.FreqWaveChart_Copy();
                Clipboard.SetDataObject(array);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            if (activeChild == null) { return; } // if no child, break
            if (activeChild.GetType() == typeof(InputForm))
            {
                InputForm chartInput = (InputForm)activeChild; // become an InputForm
                chartInput.FreqWaveChart_Paste(Clipboard.GetDataObject());
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            if (activeChild == null) { return; } // if no child, break
            if (activeChild.GetType() == typeof(InputForm))
            {
                InputForm chartInput = (InputForm)activeChild; // become an InputForm
                double[] array = chartInput.FreqWaveChart_Cut();
                Clipboard.SetDataObject(array);
            }
        }

        private void SoundWav_Load(object sender, EventArgs e)
        {

        }

        private void cascadeWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void horizontalWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void verticalWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }
    }
}
