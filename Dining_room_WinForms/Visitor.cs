using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dining_room_WinForms
{
    public class Visitor
    {
        private int rows = 72;
        private int cols = 50;

        public Visitor(int posX, int posY, string name, int money, int spontaneity, bool socialize)
        {
            this.posX = posX;
            this.posY = posY;
            this.posX = posX;
            this.posY = posY;
            this.name = name;
            this.money = money;
            this.spontaneity = spontaneity;
            this.socialize = socialize;
        }
        public int posX;
        public int posY;
        public string name;
        public int money;
        public bool socialize;
        public int spontaneity;

        public void MoveLeft()
        {
            posX--;
        }
        public void MoveRight()
        {
            posX++;
        }
        public void MoveTop()
        {
            posY--;
        }
        public void MoveBottom()
        {
            posY++;
        }
        private bool CheckPosLeft()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (j == j)
                    {

                    }
                }
            }



            if (posX == 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckPosRight()
        {
            if (posX == 24)
            {
                return false;
            }
            return true;
        }
        private bool CheckPosTop()
        {
            if (posY == 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckPosBottom()
        {
            if (posY == 40)
            {
                return false;
            }
            return true;
        }

        public void MoveTo(int direction)
        {
            switch (direction)
            {
                case 0:
                    if (CheckPosLeft())
                    {
                        MoveLeft();
                    }
                    break;
                case 1:
                    if (CheckPosRight())
                    {
                        MoveRight();
                    }
                    break;
                case 2:
                    if (CheckPosTop())
                    {
                        MoveTop();
                    }
                    break;
                case 3:
                    if (CheckPosBottom())
                    {
                        MoveBottom();
                    }
                    break;
            }
        }

        public int Move(int[,] matrix, int random)
        {
            switch (random)
            {
                case 0:
                    if (CheckPosLeft())
                    {
                        MoveLeft();
                    }
                    break;
                case 1:
                    if (CheckPosRight())
                    {
                        MoveRight();
                    }
                    break;
                case 2:
                    if (CheckPosTop())
                    {
                        MoveTop();
                    }
                    break;
                case 3:
                    if (CheckPosBottom())
                    {
                        MoveBottom();
                    }
                    break;
            }
            return random;
        }


    }
}
