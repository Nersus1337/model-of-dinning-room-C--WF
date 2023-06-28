using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dining_room_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Visitor> visitors = new List<Visitor>();

        private Graphics graphics;
        private int resolution = 20;
        private int[,] matrix;
        private int rows;
        private int cols;

        private void StartGame()
        {
            rows = (int)pictureBox1.Height / resolution;
            rows = 72;
            cols = (int)pictureBox1.Width / resolution;
            cols = 50;
            matrix = new int[rows, cols];
            //InitDefaultMap();
            visitors.Add(new Visitor(9, 9, "Maksim", 8000, 2, false));
            timer1.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void InitDefaultMap()
        {
            //Очистка всей столовой
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            //Cоздание верхней стены
            for (int i = 0; i < rows; i++)
            {
                matrix[i, 0] = 1;
            }
            //Cоздание нижней стены
            for (int i = 0; i < rows; i++)
            {
                matrix[i, cols-1] = 1;
            }
            //Cоздание левой стены
            for (int j = 0; j < cols; j++)
            {
                matrix[0, j] = 1;
            }
            //Cоздание правой стены
            for (int j = 0; j < cols; j++)
            {
                matrix[rows-1, j] = 1;
            }


            //Cоздание зоны раздачи стены
            for (int i = 0; i < rows; i++)
            {
                matrix[i, 17] = 1;
            }
            //Cоздание разделения мойки от кухни
            for (int j = 0; j < cols; j++)
            {
                matrix[rows - 18, j] = 1;
            }

        }

        private void InitCanteenItems()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        private void ClearPictureBox()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        private void NextStep()
        {
            ClearPictureBox();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            InitDefaultMap();

            //Random random = new Random();
            //int lastMove = -1;

            foreach (Visitor visitor in visitors.ToList())
            {
                matrix[visitor.posX, visitor.posY] = 2;
            }

            ///////////////////////



            int countVisitors = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        graphics.FillRectangle(Brushes.Black, i * resolution, j * resolution, resolution, resolution);
                        //graphics.DrawString(Convert.ToString(visitors[countVisitors].name), new Font("Madani Thin", 8), Brushes.DarkRed, new PointF(i * resolution, j * resolution));
                        countVisitors++;
                    }


                }
            }




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextStep();
        }
    }
}
