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
            this.name = name;
            this.money = money;
            this.spontaneity = spontaneity;
            this.socialize = socialize;
            this.costOfOrder = 0;

            this.haveTray = false;
            this.haveForksSpoons = false;
            this.haveDrink = false;
            this.haveDishes = false;
            this.hungry = true;
            this.paid = false;

        }
        public int posX;
        public int posY;
        public string name;
        public int money;
        public bool socialize;
        public int spontaneity;
        public int costOfOrder;

        public bool haveTray;
        public bool haveForksSpoons;
        public bool haveDrink;
        public bool haveDishes;
        public bool hungry;
        public bool paid;

        public int skipSteps = 0;

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

        public int MoveTo(int[,] matrix, int destX, int destY)
        {
            int thisStep = 0;
            if (skipSteps == 0)
            {
                if (posY > destY)
                {
                    MoveTop();
                    thisStep = 1;
                    return thisStep;
                }
                if (posX > destX)
                {
                    MoveLeft();
                    thisStep = 2;
                    return thisStep;
                }
                if (posY < destY)
                {
                    MoveBottom();
                    thisStep = 3;
                    return thisStep;
                }
                if (posX < destX)
                {
                    MoveRight();
                    thisStep = 4;
                    return thisStep;
                }
            }
            else
            {
                skipSteps--;
            }
            return thisStep;
        }

        public void takeDishes()
        {
            skipSteps = 5;//200
        }

        public void eat()
        {
            skipSteps = 30;//200
        }

        public void pay()
        {
            skipSteps = 10;//200
        }

        public void MoveDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    break;
                case 1:
                    MoveTop();
                    break; 
                case 2:
                    MoveLeft();
                    break; 
                case 3:
                    MoveBottom();
                    break; 
                case 4:
                    MoveRight();
                    break;
                    default:
                    break;
            }
        }

        internal void takeTray()
        {
            skipSteps = 7;//200
        }

        internal void takeDrink()
        {
            haveDrink = true;
        }
    }
}
