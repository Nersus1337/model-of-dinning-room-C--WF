using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Dining_room_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnEditCanteen.Visible = false;
        }
        private void ClearPictureBox()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        List<Visitor> visitors = new List<Visitor>();
        List<Dish> dishes = new List<Dish>();
        List<CitchenDish> citchenDishes = new List<CitchenDish>();
        List<Table> tables = new List<Table>();
        List<Drink> drinks = new List<Drink>();
        List<Cook> cooks = new List<Cook>();
        List<Washer> washers = new List<Washer>();
        Random random = new Random();

        private Graphics graphics;
        private int resolution = 20;
        private int[,] matrix;
        private int rows;
        private int cols;
        private int trays = 0;//Подносы
        private int forksSpoons = 0;//Вилки, ложки
        string currentDish = "None";
        private int cash = 0;
        private int dirtyTrays = 0;
        private bool editMap = false;
        private bool userMap = false;
        private int numberVisitors = 0;

        private void StartGame()
        {
            rows = (int)pictureBox1.Height / resolution;
            rows = 72;
            cols = (int)pictureBox1.Width / resolution;
            cols = 50;
            matrix = new int[rows, cols];
            InitCanteenItems();
            editMap = false;

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
                matrix[i, 18] = 1;
            }
            //Cоздание разделения мойки от кухни
            for (int j = 0; j < cols; j++)
            {
                matrix[rows - 18, j] = 1;
            }
            //Убираем лишнюю стену при разделении мойки от кухни в зале
            for (int j = 19; j < cols - 1; j++)
            {
                matrix[rows - 18, j] = 0;
            }

                matrix[25, 5] = 11;
                matrix[25, 6] = 11;
                matrix[25, 7] = 11;
                matrix[25, 8] = 11;
                matrix[25, 9] = 11;

                matrix[26, 5] = 11;
                matrix[26, 6] = 11;
                matrix[26, 7] = 11;
                matrix[26, 8] = 11;
                matrix[26, 9] = 11;


                matrix[31, 5] = 11;
                matrix[31, 6] = 11;
                matrix[31, 7] = 11;
                matrix[31, 8] = 11;
                matrix[31, 9] = 11;

                matrix[32, 5] = 11;
                matrix[32, 6] = 11;
                matrix[32, 7] = 11;
                matrix[32, 8] = 11;
                matrix[32, 9] = 11;


                matrix[37, 5] = 11;
                matrix[37, 6] = 11;
                matrix[37, 7] = 11;
                matrix[37, 8] = 11;
                matrix[37, 9] = 11;

                matrix[38, 5] = 11;
                matrix[38, 6] = 11;
                matrix[38, 7] = 11;
                matrix[38, 8] = 11;
                matrix[38, 9] = 11;


                matrix[43, 5] = 11;
                matrix[43, 6] = 11;
                matrix[43, 7] = 11;
                matrix[43, 8] = 11;
                matrix[43, 9] = 11;

                matrix[44, 5] = 11;
                matrix[44, 6] = 11;
                matrix[44, 7] = 11;
                matrix[44, 8] = 11;
                matrix[44, 9] = 11;

            //Создание кухонного стола для блюд
            for (int i = 21; i < 50; i++)
            {
                matrix[i, 12] = 11;
            }
            for (int i = 21; i < 50; i++)
            {
                matrix[i, 13] = 11;
            }

            //Создание кассы
            matrix[3, 19] = 6;

            //Создание подносов
            matrix[48, 18] = 7;

            //Создание вилок, ложек
            matrix[46, 18] = 8;

            //Создание окна мойщика
            matrix[62, 18] = 13;
        }

        private void InitCanteenItems()
        {
            //visitors.Add(new Visitor(48, 46, "Maksim", 8000, 2, false));
            //visitors.Add(new Visitor(48, 47, "Grigory", 8000, 3, true));
            //visitors.Add(new Visitor(48, 48, "Efim", 18000, 7, true));

            dishes.Add(new Dish(28, 18, "Салат", 180, 8, 20));
            dishes.Add(new Dish(30, 18, "Котлета", 100, 7, 20));
            dishes.Add(new Dish(32, 18, "Суп", 80, 4, 20));
            dishes.Add(new Dish(34, 18, "Лапша", 180, 8, 20));
            dishes.Add(new Dish(36, 18, "Пюре", 100, 7, 20));

            drinks.Add(new Drink(39, 18, "Чай", 40, 6, 20));
            drinks.Add(new Drink(41, 18, "Кофе", 50, 8, 20));
            drinks.Add(new Drink(43, 18, "Вода", 20, 2, 20));

            citchenDishes.Add(new CitchenDish(22, 13, "Салат", 100));
            citchenDishes.Add(new CitchenDish(24, 13, "Котлета", 100));
            citchenDishes.Add(new CitchenDish(26, 13, "Суп", 100));
            citchenDishes.Add(new CitchenDish(28, 13, "Лапша", 100));
            citchenDishes.Add(new CitchenDish(30, 13, "Пюре", 100));

            citchenDishes.Add(new CitchenDish(39, 13, "Чай", 100));
            citchenDishes.Add(new CitchenDish(41, 13, "Кофе", 100));
            citchenDishes.Add(new CitchenDish(43, 13, "Вода", 100));

            citchenDishes.Add(new CitchenDish(46, 13, "Подносы", 100));
            citchenDishes.Add(new CitchenDish(48, 13, "Вилки, ложки", 100));

            if (userMap == false)
            {
                //tables.Add(new Table(5, 30));
                //tables.Add(new Table(9, 30));
                //tables.Add(new Table(13, 30));
                //tables.Add(new Table(17, 30));
                //tables.Add(new Table(21, 30));
                //tables.Add(new Table(25, 30));
                //tables.Add(new Table(29, 30));
                //tables.Add(new Table(33, 30));
            }

            cooks.Add(new Cook(10, 10));
            //cooks.Add(new Cook(48, 47));

            washers.Add(new Washer(66, 10));

            trays = 20;//Подносы
            forksSpoons = 20;//Вилки ложки


        }

        private void getVisitor(int numberVisitors)
        {
            switch (numberVisitors)
            {
                case 0:
                    visitors.Add(new Visitor(48, 49, "Максим", 8000, 2, false));
                    break;
                case 1:
                    visitors.Add(new Visitor(48, 49, "Григорий", 8000, 3, true));
                    break;
                case 2:
                    visitors.Add(new Visitor(48, 49, "Ефим", 18000, 7, true));
                    break;
                case 3:
                    visitors.Add(new Visitor(48, 49, "Аркадий", 8000, 2, false));
                    break;
                case 4:
                    visitors.Add(new Visitor(48, 49, "Владислав", 8000, 2, false));
                    break;
                case 5:
                    visitors.Add(new Visitor(48, 49, "Илья", 8000, 2, false));
                    break;
                case 6:
                    visitors.Add(new Visitor(48, 49, "Богдан", 8000, 2, false));
                    break;
                case 7:
                    visitors.Add(new Visitor(48, 49, "Михаил", 8000, 2, false));
                    break;
                case 8:
                    visitors.Add(new Visitor(48, 49, "Петр", 8000, 2, false));
                    break;
                case 9:
                    visitors.Add(new Visitor(48, 49, "Ян", 8000, 2, false));
                    break;
                case 10:
                    visitors.Add(new Visitor(48, 49, "Давид", 8000, 2, false));
                    break;
                case 11:
                    visitors.Add(new Visitor(48, 49, "Валерий", 8000, 2, false));
                    break;
                case 12:
                    visitors.Add(new Visitor(48, 49, "Александр", 8000, 3, true));
                    break;
                case 13:
                    visitors.Add(new Visitor(48, 49, "Алексей", 18000, 7, true));
                    break;
                case 14:
                    visitors.Add(new Visitor(48, 49, "Наталья", 8000, 2, false));
                    break;
                case 15:
                    visitors.Add(new Visitor(48, 49, "Дарья", 8000, 2, false));
                    break;
                case 16:
                    visitors.Add(new Visitor(48, 49, "Валерия", 8000, 2, false));
                    break;
                case 17:
                    visitors.Add(new Visitor(48, 49, "Ангелина", 8000, 2, false));
                    break;
                case 18:
                    visitors.Add(new Visitor(48, 49, "Анастасия", 8000, 2, false));
                    break;
                case 19:
                    visitors.Add(new Visitor(48, 49, "Александра", 8000, 2, false));
                    break;
                case 20:
                    visitors.Add(new Visitor(48, 49, "Аня", 8000, 2, false));
                    break;
                case 21:
                    visitors.Add(new Visitor(48, 49, "Татьяна", 8000, 2, false));
                    break;
                case 22:
                    visitors.Add(new Visitor(48, 49, "Виктория", 8000, 2, false));
                    break;
                case 23:
                    visitors.Add(new Visitor(48, 49, "Елизавета", 8000, 3, true));
                    break;
                case 24:
                    visitors.Add(new Visitor(48, 49, "Ксения", 18000, 7, true));
                    break;
                case 25:
                    visitors.Add(new Visitor(48, 49, "Вероника", 8000, 2, false));
                    break;
                case 26:
                    visitors.Add(new Visitor(48, 49, "Евгения", 8000, 2, false));
                    break;
                case 27:
                    visitors.Add(new Visitor(48, 49, "Евгений", 8000, 2, false));
                    break;
                case 28:
                    visitors.Add(new Visitor(48, 49, "Антон", 8000, 2, false));
                    break;
                case 29:
                    visitors.Add(new Visitor(48, 49, "Игорь", 8000, 2, false));
                    break;
                case 30:
                    visitors.Add(new Visitor(48, 49, "Ахмед", 8000, 2, false));
                    break;
                case 31:
                    visitors.Add(new Visitor(48, 49, "Джон", 8000, 2, false));
                    break;
                case 32:
                    visitors.Add(new Visitor(48, 49, "Афродита", 8000, 2, false));
                    break;
                case 33:
                    visitors.Add(new Visitor(48, 49, "Захар", 8000, 2, false));
                    break;
                case 34:
                    visitors.Add(new Visitor(48, 49, "Данил", 8000, 3, true));
                    break;
                case 35:
                    visitors.Add(new Visitor(48, 49, "Артур", 18000, 7, true));
                    break;
                case 36:
                    visitors.Add(new Visitor(48, 49, "Иван", 8000, 2, false));
                    break;
                case 37:
                    visitors.Add(new Visitor(48, 49, "Афанасий", 8000, 2, false));
                    break;
                case 38:
                    visitors.Add(new Visitor(48, 49, "Виктор", 8000, 2, false));
                    break;
                case 39:
                    visitors.Add(new Visitor(48, 49, "Полина", 8000, 2, false));
                    break;
                case 40:
                    visitors.Add(new Visitor(48, 49, "Юлия", 8000, 2, false));
                    break;
                case 41:
                    visitors.Add(new Visitor(48, 49, "Ольга", 8000, 2, false));
                    break;
                case 42:
                    visitors.Add(new Visitor(48, 49, "Надежда", 8000, 2, false));
                    break;
                case 43:
                    visitors.Add(new Visitor(48, 49, "Лина", 8000, 2, false));
                    break;
            }
        }

        private void NextStep()
        {
            ClearPictureBox();
            InitDefaultMap();

            //Случайное появление посетителя
            if (random.Next(25) == 1)
            {
                getVisitor(numberVisitors);
                numberVisitors++;
                if (numberVisitors == 44)
                {
                    numberVisitors = 0;
                }
            }

            //Запись координат блюд
            foreach (Dish dish in dishes.ToList())
            {
                matrix[dish.posX, dish.posY] = 3;
            }
            //Запись координат напитков
            foreach (Drink drink in drinks.ToList())
            {
                matrix[drink.posX, drink.posY] = 9;
            }
            //Запись координат столиков
            foreach (Table table in tables.ToList())
            {
                matrix[table.posX, table.posY] = 4;
            }
            //Запись координат поваров
            foreach (Cook cook in cooks.ToList())
            {
                matrix[cook.posX, cook.posY] = 10;
            }
            //Запись координат блюд на кухне
            foreach (CitchenDish citchenDish in citchenDishes.ToList())
            {
                matrix[citchenDish.posX, citchenDish.posY] = 12;
            }
            //Запись координат мойщиков
            foreach (Washer washer in washers.ToList())
            {
                matrix[washer.posX, washer.posY] = 14;
            }
            //Координаты временной метки
            matrix[48, 48] = 19;

            ///////////////////////////////////////////
            ///////////////////////////////////////////
            ///////////////////////////////////////////
            
            //IMPORTANT
            //Движение посетителей
            foreach (Visitor visitor in visitors.ToList())
            {
                bool flag = true;//Свободно ли в следующей клетке

                //Посетитель заходит в столовую и сразу идет за подносом
                if ((visitor.haveDishes == false) && (visitor.hungry == true) && (visitor.paid == false) && (visitor.haveTray == false))
                {
                    foreach (Visitor otherVisitor in visitors)
                    {
                        if ((otherVisitor.posY == visitor.posY - 1) && (otherVisitor.posX == 48))
                        {
                            flag = false;
                        }
                    }
                    if (flag==true)
                    {
                        visitor.MoveTo(matrix, 48, 19);
                        if ((visitor.posX == 48) && (visitor.posY == 19))
                        {
                            if (trays > 0)
                            {
                                visitor.haveTray = true;
                                trays--;
                                visitor.takeTray();
                            }

                        }
                    }
                }
                flag = true;
                //Посетитель идет за вилками ложками
                if ((visitor.haveDishes == false) && (visitor.hungry == true) && (visitor.paid == false) && (visitor.haveTray == true) && (visitor.haveForksSpoons == false))
                {
                    foreach (Visitor otherVisitor in visitors)
                    {
                        if ((otherVisitor.posX == visitor.posX - 1))
                        {
                            flag = false;
                        }
                    }
                    if (flag == true)
                    {
                        visitor.MoveTo(matrix, 46, 19);
                        if ((visitor.posX == 46) && (visitor.posY == 19))
                        {
                            if (forksSpoons > 0)
                            {
                                visitor.haveForksSpoons = true;
                                forksSpoons--;
                                visitor.takeTray();
                            }

                        }
                    }
                }
                flag = true;
                //Посетитель выбирает напитки
                if ((visitor.haveDishes == false) && (visitor.hungry == true) && (visitor.paid == false) && (visitor.haveTray == true) && (visitor.haveForksSpoons == true) && (visitor.haveDrink == false))
                {
                    int chooseDrink = random.Next(11);
                    string nameDrink = "Water";
                    int coordXDrink = -1;
                    int coordYDrink = -1;
                    foreach (Drink drink in drinks)
                    {
                        if ((drink.taste > chooseDrink) && (visitor.haveDrink == false))
                        {
                            coordXDrink = drink.posX;
                            coordYDrink = drink.posY;
                            nameDrink = drink.name;
                            break;
                        }
                    }
                    if (coordXDrink == -1)//Если посетитель ничего не выбрал, то он пьет воду
                    {
                        coordXDrink = 43;
                        coordYDrink = 18;
                        nameDrink = "Вода";
                    }
                    foreach (Visitor otherVisitor in visitors)
                    {
                        if ((otherVisitor.posX == visitor.posX - 1))
                        {
                            flag = false;
                        }
                    }
                    if (flag == true)
                    {
                        visitor.MoveTo(matrix, coordXDrink, coordYDrink + 1);
                        if ((visitor.posX == coordXDrink) && (visitor.posY == coordYDrink + 1))
                        {
                            foreach (Drink drink in drinks)
                            {
                                if (drink.name == nameDrink)
                                {
                                    if (drink.count > 0)
                                    {
                                        drink.count--;
                                        visitor.costOfOrder = visitor.costOfOrder + drink.cost;
                                        visitor.takeDrink();
                                        visitor.haveDrink = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                flag = true;
                //Посетитель выбирает блюда
                if ((visitor.haveDishes == false) && (visitor.hungry == true) && (visitor.paid == false) && (visitor.haveTray == true) && (visitor.haveForksSpoons == true) && (visitor.haveDrink == true))
                {
                    int chooseDrink = random.Next(11);
                    string nameDrink = "Water";
                    int coordXDrink = -1;
                    int coordYDrink = -1;
                    foreach (Dish dish in dishes)
                    {
                        if ((dish.taste > chooseDrink) && (visitor.haveDishes == false))
                        {
                            coordXDrink = dish.posX;
                            coordYDrink = dish.posY;
                            nameDrink = dish.name;
                            break;
                        }
                    }
                    if (coordXDrink == -1)//Если посетитель ничего не выбрал, то он ест суп
                    {
                        coordXDrink = 32;
                        coordYDrink = 18;
                        nameDrink = "Суп";
                    }
                    foreach (Visitor otherVisitor in visitors)
                    {
                        if ((otherVisitor.posX == visitor.posX - 1))
                        {
                            flag = false;
                        }
                    }
                    if (flag == true)
                    {
                        visitor.MoveTo(matrix, coordXDrink, coordYDrink + 1);
                        if ((visitor.posX == coordXDrink) && (visitor.posY == coordYDrink + 1))
                        {
                            foreach (Dish dish in dishes)
                            {
                                if (dish.name == nameDrink)
                                {
                                    if (dish.count > 0)
                                    {
                                        visitor.costOfOrder = visitor.costOfOrder + dish.cost;
                                        visitor.haveDishes = true;
                                        dish.count--;
                                        visitor.takeDishes();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                flag = true;
                //Посетитель оплачивает блюда
                if ((visitor.haveDishes == true) && (visitor.hungry == true) && (visitor.paid == false) && (visitor.haveDrink == true))
                {
                    foreach (Visitor otherVisitor in visitors)
                    {
                        if ((otherVisitor.posX == visitor.posX - 1) && (otherVisitor.posY == visitor.posY))
                        {
                            flag = false;
                        }
                    }
                    if (flag == true)
                    {
                        visitor.MoveTo(matrix, 3, 19);
                        if ((visitor.posX == 3) && (visitor.posY == 19))
                        {
                            visitor.money = visitor.money - visitor.costOfOrder;
                            cash = cash + visitor.costOfOrder;
                            visitor.paid = true;
                            visitor.pay();
                        }
                    }
                }

                foreach (Table table in tables)
                {
                    foreach (Visitor visitorTable in visitors)
                    {
                        if ((visitorTable.posX == table.posX) && (visitorTable.posY == table.posY))
                        {
                            table.empty = false;
                            break;
                        }
                    }
                }

                    flag = true;
                //Посетитель идет с едой за стол
                if ((visitor.haveDishes == true) && (visitor.hungry == true) && (visitor.paid == true))
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
                                //visitor.haveDishes = false;
                                //table.empty = false;
                                visitor.eat();
                            }
                            break;
                        }
                    }
                        
                }
                foreach (Table table in tables)
                {
                    table.empty = true;
                }
                    flag = true;
                //Убирает поднос
                if ((visitor.haveDishes == true) && (visitor.hungry == false) && (visitor.paid == true))
                {
                    visitor.MoveTo(matrix, 62, 18 + 1);
                    if ((visitor.posX == 62) && (visitor.posY == 18 + 1))
                    {
                        visitor.haveDishes = false;
                        dirtyTrays++;
                        //visitor.haveTray = false;
                    }
                }
                flag = true;
                //Если посетитель поел, то он уходит
                if ((visitor.hungry == false) && (visitor.haveDishes == false) && (visitor.paid == true))
                {
                    visitor.MoveTo(matrix, 50, 48);//На выход
                    if ((visitor.posX == 50) && (visitor.posY == 48))
                    {
                        visitors.Remove(visitor);
                    }
                }
            }


            foreach (Cook cook in cooks)
            {
                if (currentDish == "None") 
                {
                    foreach (Dish dish in dishes)
                    {
                        if (dish.count < 10)
                        {
                            currentDish = dish.name;
                            break;
                        }
                    }
                    if (currentDish == "None")
                    {
                        if (trays < 10)
                        {
                            currentDish = "Trays";
                            //break;
                        }
                    }
                    if (currentDish == "None")
                    {
                        if (forksSpoons < 10)
                        {
                            currentDish = "forksSpoons";
                        }
                    }

                    if (currentDish == "None")
                    {
                        foreach (Drink drink in drinks)
                        {
                            if (drink.count < 10)
                            {
                                currentDish = drink.name;
                                break;
                            }
                        }
                    }
                    

                }
                
                if ((currentDish == "Trays") && (cook.haveDishes == false))
                {
                    cook.MoveTo(48, 13 + 1);
                    if ((cook.posX == 48) && (cook.posY == 13 + 1))
                    {
                        cook.haveDishes = true;
                    }
                }

                if ((currentDish == "Trays") && (cook.haveDishes == true))
                {
                    cook.MoveTo(48, 18 - 1);
                    if ((cook.posX == 48) && (cook.posY == 18 - 1))
                    {
                        cook.haveDishes = false;
                        currentDish = "None";
                        trays = 20;
                    }
                }

                if ((currentDish == "forksSpoons") && (cook.haveDishes == false))
                {
                    cook.MoveTo(46, 13 + 1);
                    if ((cook.posX == 46) && (cook.posY == 13 + 1))
                    {
                        cook.haveDishes = true;
                    }
                }

                if ((currentDish == "forksSpoons") && (cook.haveDishes == true))
                {
                    cook.MoveTo(46, 18 - 1);
                    if ((cook.posX == 46) && (cook.posY == 18 - 1))
                    {
                        cook.haveDishes = false;
                        currentDish = "None";
                        forksSpoons = 20;
                    }
                }

                foreach (CitchenDish citchendish in citchenDishes)
                {
                    if ((citchendish.name == currentDish) && (cook.haveDishes == false))
                    {
                        cook.MoveTo(citchendish.posX, citchendish.posY + 1);
                        if ((cook.posX == citchendish.posX) && (cook.posY == citchendish.posY + 1))
                        {
                            cook.haveDishes = true;
                        }
                        break;
                    }
                }
                
                if (cook.haveDishes == true)
                {
                    foreach (Dish dish in dishes)
                    {
                        if (dish.name == currentDish)
                        {
                            cook.MoveTo(dish.posX, dish.posY - 1);
                            if ((cook.posX == dish.posX) && (cook.posY == dish.posY - 1))
                            {
                                cook.haveDishes = false;
                                dish.count = 20;
                                currentDish = "None";
                            }
                            break;
                        }
                        
                    }
                    foreach (Drink drink in drinks)
                    {
                        if (drink.name == currentDish)
                        {
                            cook.MoveTo(drink.posX, drink.posY - 1);
                            if ((cook.posX == drink.posX) && (cook.posY == drink.posY - 1))
                            {
                                cook.haveDishes = false;
                                drink.count = 20;
                                currentDish = "None";
                            }
                            break;
                        }

                    }
                }

            }

            foreach (Washer washer in washers)
            {
                if (dirtyTrays > 0)
                {
                    washer.MoveTo(62, 18 - 1);
                    if ((washer.posX == 62) && (washer.posY == 18 - 1))
                    {
                        washer.takeTrays();
                        washer.haveTrays = true;
                        dirtyTrays = 0;
                    }
                }
                if (washer.haveTrays == true)
                {
                    washer.MoveTo(60 + 1, 10);
                    if ((washer.posX == 60 + 1) && (washer.posY == 10))
                    {
                        washer.washingTrays();
                        washer.haveTrays = false;
                        
                    }
                }
            }





            //Запись координат посетителей
            foreach (Visitor visitor in visitors.ToList())
            {
                matrix[visitor.posX, visitor.posY] = 2;
            }
            
            
            int countVisitors = 0;



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
                        graphics.DrawString(Convert.ToString(visitors[countVisitors].name), new Font("Madani Thin", 8), Brushes.DarkRed, new PointF(i * resolution, j * resolution));
                        countVisitors++;
                    }
                    //Рисование блюд
                    if (matrix[i, j] == 3)
                    {
                        graphics.FillRectangle(Brushes.PaleVioletRed, i * resolution, j * resolution, resolution, resolution);
                        foreach (Dish dish in dishes)
                        {
                            graphics.DrawString(Convert.ToString(dish.name), new Font("Madani Thin", 8), Brushes.Black, new PointF(dish.posX * resolution, dish.posY * resolution));
                            graphics.DrawString(Convert.ToString(dish.count), new Font("Madani Thin", 8), Brushes.Black, new PointF(dish.posX * resolution, (dish.posY + (float)0.45) * resolution));
                        }
                    }
                    //Рисование столов
                    if (matrix[i, j] == 4)
                    {
                        graphics.FillRectangle(Brushes.Gray, i * resolution, j * resolution, resolution, resolution);
                    }

                    //Рисование мойщика


                    //Рисование кассы
                    if (matrix[i, j] == 6)
                    {
                        graphics.FillRectangle(Brushes.Blue, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Рисование подносов
                    if (matrix[i, j] == 7)
                    {
                        graphics.FillRectangle(Brushes.LightGray, i * resolution, j * resolution, resolution, resolution);
                        graphics.DrawString("Подносы", new Font("Madani Thin", 8), Brushes.Black, new PointF(48 * resolution, 18 * resolution));
                        graphics.DrawString(Convert.ToString(trays), new Font("Madani Thin", 8), Brushes.Black, new PointF(48 * resolution, (18 + (float)0.45) * resolution));
                    }
                    //Рисование вилок, ложек
                    if (matrix[i, j] == 8)
                    {
                        graphics.FillRectangle(Brushes.LightGray, i * resolution, j * resolution, resolution, resolution);
                        graphics.DrawString("Приборы", new Font("Madani Thin", 8), Brushes.Black, new PointF(46 * resolution, 18 * resolution));
                        graphics.DrawString(Convert.ToString(forksSpoons), new Font("Madani Thin", 8), Brushes.Black, new PointF(46 * resolution, (18 + (float)0.45) * resolution));
                    }
                    //Рисование напитков
                    if (matrix[i, j] == 9)
                    {
                        graphics.FillRectangle(Brushes.LightSkyBlue, i * resolution, j * resolution, resolution, resolution);
                        foreach (Drink drink in drinks)
                        {
                            graphics.DrawString(Convert.ToString(drink.name), new Font("Madani Thin", 8), Brushes.Black, new PointF(drink.posX * resolution, drink.posY * resolution));
                            graphics.DrawString(Convert.ToString(drink.count), new Font("Madani Thin", 8), Brushes.Black, new PointF(drink.posX * resolution, (drink.posY + (float)0.45) * resolution));
                        }
                    }
                    //Рисование поваров
                    if (matrix[i, j] == 10)
                    {
                        graphics.FillEllipse(Brushes.Green, i * resolution, j * resolution, resolution, resolution);
                        //graphics.DrawString(Convert.ToString(visitors[countVisitors].name), new Font("Madani Thin", 8), Brushes.DarkRed, new PointF(i * resolution, j * resolution));
                        //countVisitors++;
                    }
                    //Рисование столов на кухне
                    if (matrix[i, j] == 11)
                    {
                        graphics.FillRectangle(Brushes.Gray, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Рисование блюд на столах на кухне
                    if (matrix[i, j] == 12)
                    {
                        graphics.FillRectangle(Brushes.IndianRed, i * resolution, j * resolution, resolution, resolution);
                        foreach (CitchenDish citchenDish in citchenDishes)
                        {
                            graphics.DrawString(Convert.ToString(citchenDish.name), new Font("Madani Thin", 8), Brushes.Black, new PointF(citchenDish.posX * resolution, citchenDish.posY * resolution));
                            graphics.DrawString(Convert.ToString(citchenDish.count), new Font("Madani Thin", 8), Brushes.Black, new PointF(citchenDish.posX * resolution, (citchenDish.posY + (float)0.45) * resolution));
                        }
                    }
                    //Рисование окна мойщика
                    if (matrix[i, j] == 13)
                    {
                        graphics.FillRectangle(Brushes.DarkGray, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Рисование мойщика
                    if (matrix[i, j] == 14)
                    {
                        graphics.FillRectangle(Brushes.Blue, i * resolution, j * resolution, resolution, resolution);
                    }
                    //Временная метка
                    if (matrix[i, j] == 19)
                    {
                        graphics.FillEllipse(Brushes.Yellow, i * resolution, j * resolution, resolution, resolution);
                        graphics.DrawString(Convert.ToString(cash), new Font("Madani Thin", 8), Brushes.Black, new PointF((2 - 1) * resolution, 19 * resolution));
                    }

                }
            }

            


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextStep();
        }

        private void btnEditCanteen_Click(object sender, EventArgs e)
        {
            editMap = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (editMap == true)
            {
                graphics.FillRectangle(Brushes.Gray, e.X / resolution * resolution, e.Y / resolution * resolution, resolution, resolution);
                graphics.FillRectangle(Brushes.Red, 5 * resolution, 5 * resolution, resolution, resolution);
                pictureBox1.Refresh();
                //pictureBox1.Update();
                userMap = true;
                tables.Add(new Table(e.X / resolution, e.Y / resolution));
                
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnEditCanteen.Visible = true;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            timer1.Start();
            btnEditCanteen.Visible = false;
            editMap = false;
        }

        private void trbTimer_Scroll(object sender, EventArgs e)
        {
            //timer1.Interval = trbTimer.Value();
        }
    }
}
