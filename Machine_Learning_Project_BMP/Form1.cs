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
        number_image[] images = new number_image[10];
        public Form1()
        {
            
            InitializeComponent();

            //Ловим ресурсы для обучения в листбокс

            FolderBrowserDialog pathselector = new FolderBrowserDialog();
            pathselector.ShowNewFolderButton = false;
            if (!(pathselector.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("Error");
            }
              
            resfolder = new DirectoryInfo(pathselector.SelectedPath);
            FileInfo[] fileInfo = resfolder.GetFiles("*.bmp");

            //Подгрузка весов перцептронов
            /*for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                    input[j,i]
            }*/
            //
            foreach ( var file in fileInfo)
            {
                listBox1.Items.Add(file);
                
            }

            TopMost = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Распознавание элементов картинки, здесь нужно создать образ картинки

            //
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
    public class number_image
    {
        public int[,] image;
        public int[,] input;
        public int floor = 10;

        public int count_weight(int[,] cur_inp)
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                    sum += image[i, j] * cur_inp[i, j];
            }
            return sum;
        }
        public number_image(int width, int height, int[,] image_in)
        {
            image = new int[width, height];
            image = image_in;
        }

    }
}
