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
        FolderBrowserDialog pathselector = new FolderBrowserDialog();
        OpenFileDialog pictureselector = new OpenFileDialog();
       
        int found_number;
        int[,] _current_image = new int[5, 8];

        public Form1()
        {

            InitializeComponent();
           
            //Ловим ресурсы для обучения в листбокс

            pathselector.ShowNewFolderButton = false;
            pathselector.SelectedPath= Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),@"..\..\" ) + "resfolder");
            
            
          
            /*if (!(pathselector.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("Error");
            }*/

            resfolder = new DirectoryInfo(pathselector.SelectedPath);
            FileInfo[] fileInfo = resfolder.GetFiles("*.bmp");
            StreamReader reader = new StreamReader(pathselector.SelectedPath + "/weights.txt");
            
            //Text = ;
            //Подгрузка весов перцептронов
            for (int i = 0; i < 10; i++)
            {
                int[,] tmp_image = new int[5, 8];

                string num = reader.ReadLine();
                for (int j = 0; j < 8; j++)
                {
                   
                    string[] arr = reader.ReadLine().Split(' ');
                    for (int k = 0; k < 5; k++)
                        tmp_image[k,j] = int.Parse(arr[k]);
                }
                images[i] = new number_image(5, 8, tmp_image);
                //
            }
            reader.Close();

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
            
            MessageBox.Show("Обучение, 25 прогонов по директории");
            for (int kek=0; kek<=25; kek++)
            {
                
                int count = 0;
                int false_num = 0;
                foreach (var file in listBox1.Items)
                {
                    count++;

                    FileInfo straw = (FileInfo)file;
                    if (straw == null) { MessageBox.Show("Выбери картинку дебил"); }
                    else
                    {
                        Bitmap current_image = (Bitmap)Image.FromFile(straw.FullName);

                        int maximum = -500;
                        for (int z = 0; z < 10; z++)
                        {
                            for (int i = 0; i < current_image.Height; i++)
                                for (int j = 0; j < current_image.Width; j++)
                                {
                                    Color color = current_image.GetPixel(j, i);
                                    if ((color.R < 225) && (color.G < 225) && (color.B < 225))
                                        _current_image[j, i] = 1;
                                    else _current_image[j, i] = 0;
                                }
                        }
                        for (int i = 0; i < 10; i++)
                        {

                            if (images[i].count_weight(_current_image) > maximum)
                            {
                                maximum = images[i].count_weight(_current_image);
                                found_number = i;
                            }
                        }
                        FileInfo file_ = (FileInfo)file;
                        string rightnum = "";
                        rightnum += file_.Name[0];
                        if (!(found_number == int.Parse(rightnum)))
                        //MessageBox.Show("Определена цифра " + found_number);
                        {
                            false_num++;
                            images[found_number].correct_false(_current_image);
                            images[int.Parse(rightnum)].correct_true(_current_image);
                        }

                    }
                    
                }
                
                richTextBox1.Text += "Процент ошибок " + kek + " ого прохода: " + Math.Truncate((double)false_num / (double)count * 100.0)  + "% \n ";
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileInfo straw = (FileInfo)listBox1.SelectedItem;
            if (straw == null) { MessageBox.Show("Выбери картинку дебил"); }
            else
            {
                Bitmap current_image = (Bitmap)Image.FromFile(straw.FullName);
               
                int maximum = -500;
                for (int z = 0; z < 10; z++)
                {
                    for (int i = 0; i < current_image.Height; i++)
                        for (int j = 0; j < current_image.Width; j++)
                        {
                            Color color = current_image.GetPixel(j, i);
                            if ((color.R < 225) && (color.G < 225) && (color.B < 225))
                                _current_image[j, i] = 1;
                            else _current_image[j, i] = 0;
                        }
                }
                for (int i=0; i<10; i++)
                {
                  
                    if(images[i].count_weight(_current_image)>maximum)
                    {
                        maximum = images[i].count_weight(_current_image);
                        found_number = i;
                    }
                }
                MessageBox.Show("Определена цифра " + found_number);

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo straw = (FileInfo)listBox1.SelectedItem;
            pictureBox1.Image = (Bitmap)Image.FromFile(straw.FullName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            images[found_number].correct_false(_current_image);
            images[(int)numericUpDown1.Value].correct_true(_current_image);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num = 0;
            StreamWriter writer = new StreamWriter (pathselector.SelectedPath + "/weights.txt");
            foreach (var img in images)
            {
                writer.WriteLine(num);
                num++;
                for (int j = 0; j < 8; j++)
                {

                    
                    for (int k = 0; k < 5; k++)
                        writer.Write(img.image[k,j]+" ");
                    writer.WriteLine();
                }

            }
            writer.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            File.Delete(pathselector.SelectedPath + "/backup.txt");
            File.Copy(pathselector.SelectedPath + "/weights.txt", pathselector.SelectedPath + "/backup.txt");
            int num = 0;
            StreamWriter writer = new StreamWriter(pathselector.SelectedPath + "/weights.txt");
            foreach (var img in images)
            {
                writer.WriteLine(num);
                num++;
                for (int j = 0; j < 8; j++)
                {


                    for (int k = 0; k < 5; k++)
                        writer.Write(0 + " ");
                    writer.WriteLine();
                }

            }
            writer.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!(pictureselector.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("Error");
            }
            pictureBox1.Image = (Bitmap)Image.FromFile(pictureselector.FileName);

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
        public void correct_false(int [,] cur_inp)
        {
            for (int i=0;i<5;i++)
            {
                for (int j=0;j<8;j++)
                {
                    image[i, j] -= cur_inp[i,j];
                }
            }
        }
        public void correct_true(int [,] cur_inp)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    image[i, j] += cur_inp[i, j];
                }
            }
        }
        public number_image(int width, int height, int[,] image_in)//создание класса перцептрона
        {
            image = new int[width, height];
            image = image_in;
        }

    }
}
