using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Machine_Learning_Project_BMP
{
    
    public partial class Form1 : Form
    {
        DirectoryInfo resfolder;

        public Form1()
        {
            
            InitializeComponent();

            FolderBrowserDialog pathselector = new FolderBrowserDialog();
            pathselector.ShowNewFolderButton = false;
            if (!(pathselector.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("Error");
            }
              
            resfolder = new DirectoryInfo(pathselector.SelectedPath);
            FileInfo[] fileInfo = resfolder.GetFiles("*.bmp");
            
            foreach ( var file in fileInfo)
            {
                listBox1.Items.Add(file);
                
            }
            TopMost = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo straw =(FileInfo) listBox1.SelectedItem;
            pictureBox1.Image = (Bitmap)Image.FromFile(straw.FullName);
        }
    }
}
