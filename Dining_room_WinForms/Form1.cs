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
        List<Table> tables = new List<Table>();

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

            dishes.Add(new Dish(20, 18, "Sup", 180, 2, 4));
            dishes.Add(new Dish(25, 18, "Salad", 100, 5, 2));
            dishes.Add(new Dish(30, 18, "Tea", 180, 2, 30));
            dishes.Add(new Dish(35, 18, "Potato", 180, 2, 1));
            dishes.Add(new Dish(33, 18, "Kotleta", 180, 2, 1));

            tables.Add(new Table(5, 30));
            tables.Add(new Table(9, 30));
            tables.Add(new Table(13, 30));
            tables.Add(new Table(17, 30));
            tables.Add(new Table(21, 30));
            tables.Add(new Table(25, 30));
            tables.Add(new Table(29, 30));
            tables.Add(new Table(33, 30));
            //tables.Add(new Table(25, 30));
            //tables.Add(new Table(25, 30));
        }

        private void NextStep()
        {
            ClearPictureBox();
            InitDefaultMap();

            //Запись координат блюд
            foreach (Dish dish in dishes.ToList())
            {
                if (dish.count != 0)//Если порции еще остались
                {
                    matrix[dish.posX, dish.posY] = 3;
                }
                else//Если порции закончились, то удаляем блюдо
                {
                    //dishes.Remove(dish);
                }
            }
            //Запись координат столиков
            foreach (Table table in tables.ToList())
            {
                matrix[table.posX, table.posY] = 4;
            }


            //Координаты временной метки
            matrix[48, 48] = 10;

            ///////////////////////////////////////////
            ///////////////////////////////////////////
            ///////////////////////////////////////////
           
            //IMPORTANT
            //Движение посетителей
            foreach (Visitor visitor in visitors)
            {
                //Посетитель заходит в столовую и сразу идет за едой
                if ((visitor.haveDishes==false) && (visitor.hungry==true))
                {
                    foreach (Dish dish in dishes)
                    {
                        if (dish.count > 0)
                        {
                            visitor.MoveTo(matrix, dish.posX, dish.posY);

                            //Посетитель берет еду
                            if ((visitor.posX == dish.posX) && (visitor.posY == dish.posY))
                            {
                                dish.count--;
                                visitor.takeDishes();
                            }

                            break;
                        }
                    }
                        
                }
                
                //Посетитель идет с едой за стол
                if ((visitor.haveDishes == true) && (visitor.hungry == true))
                {
                    foreach (Table table in tables)
                    {
                        if (table.empty == true)
                        {
                            visitor.MoveTo(matrix, table.posX, table.posY);

                            //Посетитель ест за столом
                            if ((visitor.posX == table.posX) && (visitor.posY == table.posY))
                            {
                                visitor.hungry = false;
                                visitor.haveDishes = false;
                                table.empty = false;
                                visitor.eat();
                            }
                            break;
                        }
                    }
                        
                }
                
                //Если посетитель поел, то он уходит
                if ((visitor.hungry == false) && (visitor.haveDishes == false))
                {
                    visitor.MoveTo(matrix, 48, 48);//На выход
                }
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
                        foreach (Dish dish in dishes)
                        {
                            graphics.DrawString(Convert.ToString(dish.name), new Font("Madani Thin", 8), Brushes.Black, new PointF(dish.posX * resolution, dish.posY * resolution));
                            graphics.DrawString(Convert.ToString(dish.count), new Font("Madani Thin", 8), Brushes.Black, new PointF(dish.posX * resolution, (dish.posY + (float)0.5) * resolution));
                        }
                    }
                    //Рисование столов
                    if (matrix[i, j] == 4)
                    {
                        graphics.FillRectangle(Brushes.Gray, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Временная метка
                    if (matrix[i, j] == 10)
                    {
                        graphics.FillEllipse(Brushes.Yellow, i * resolution, j * resolution, resolution, resolution);
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
