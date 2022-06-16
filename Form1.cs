using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToCbz2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Choose Manga Directory";
            // Show the FolderBrowserDialog.
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                CompileToCbz cc = new CompileToCbz(fbd.SelectedPath + "\\");
            }
        }
    }
}
