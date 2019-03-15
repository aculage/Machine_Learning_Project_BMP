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
            StreamReader reader = new StreamReader(pathselector.SelectedPath + "/weights.txt",System.Text.Encoding.Default);
            
            //Text = ;
            //Подгрузка весов перцептронов
            for (int i = 0; i < 10; i++)
            {
                int[,] tmp_image = new int[5, 8];
                int num = reader.Read();

                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 5; k++)
                        tmp_image[k,j] = ;
                }
                images[i] = new number_image(5, 8, tmp_image);
                //
            }

            /*for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 5; k++)
                    textBox1.Text += images[0].image[k,j]; 
            }*/

            foreach (var file in fileInfo)
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
            Bitmap current_image = (Bitmap)pictureBox1.Image;
            int[,] _current_image = new int[5, 8];
            for (int i=0; i<current_image.Width;i++)
                for(int j=0;j<current_image.Height;j++)
                {

                }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo straw = (FileInfo)listBox1.SelectedItem;
            pictureBox1.Image = (Bitmap)Image.FromFile(straw.FullName);
        }
    }
    public class number_image
    {
        public int[,] image;
        public int[,] input;
        public int floor = 10;

        public int count_weight(int[,] cur_inp) //Подсчет веса образа картинки на вход
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                    sum += image[i, j] * cur_inp[i, j];
            }
            return sum;
        }
        public number_image(int width, int height, int[,] image_in)//создание класса перцептрона
        {
            image = new int[width, height];
            image = image_in;
        }

    }
}
