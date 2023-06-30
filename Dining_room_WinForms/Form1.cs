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
        private void ClearPictureBox()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        List<Visitor> visitors = new List<Visitor>();
        List<Dish> dishes = new List<Dish>();

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
            InitCanteenItems();
            
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


            //Cоздание стены для зоны раздачи
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
            visitors.Add(new Visitor(47, 47, "Maksim", 8000, 2, false));
            visitors.Add(new Visitor(45, 45, "Grigory", 8000, 3, true));
            visitors.Add(new Visitor(43, 43, "Efim", 18000, 7, true));

            dishes.Add(new Dish(20, 18, "Sup", 180, 2));
            dishes.Add(new Dish(25, 18, "Salad", 100, 5));
        }

        private void NextStep()
        {
            ClearPictureBox();
            InitDefaultMap();

            Random random = new Random();
            //int lastMove = -1;

            //Запись координат блюд
            foreach (Dish dish in dishes.ToList())
            {
                matrix[dish.posX, dish.posY] = 3;
            }

            ///////////////////////////////////////////
            ///////////////////////////////////////////
            ///////////////////////////////////////////

            //Движение посетителей
            foreach (Visitor visitor in visitors)
            {
                visitor.Move(matrix, random.Next(4));
            }
            //Запись координат посетителей
            foreach (Visitor visitor in visitors.ToList())
            {
                matrix[visitor.posX, visitor.posY] = 2;
            }


            //graphics.DrawString(Convert.ToString(visitors[countVisitors].name), new Font("Madani Thin", 8), Brushes.DarkRed, new PointF(i * resolution, j * resolution));
            //countVisitors++;
            //int countVisitors = 0;
            //Отрисовка всех элементов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //Рисование стен
                    if (matrix[i, j] == 1)
                    {
                        graphics.FillRectangle(Brushes.Black, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Рисование посетителей
                    if (matrix[i, j] == 2)
                    {
                        graphics.FillEllipse(Brushes.LightSeaGreen, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Рисование блюд
                    if (matrix[i, j] == 3)
                    {
                        graphics.FillRectangle(Brushes.PaleVioletRed, i * resolution, j * resolution, resolution, resolution);
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
