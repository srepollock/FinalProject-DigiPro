using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject_v3
{
    public partial class openFileTestForm : Form
    {
        public openFileTestForm()
        {
            InitializeComponent();
        }

        public void OpenFile(string fileName)
        {
            if(fileName == "yes")
            {
                textBox.Text = "Yes";
                return;
            }
            // Open help file
            textBox.LoadFile(fileName, RichTextBoxStreamType.PlainText);
        }
        
    }
}
