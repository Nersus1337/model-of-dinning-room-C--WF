using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dining_room_WinForms
{
    public class Cook
    {
        public Cook(int posX, int posY)
        { 
            this.posX = posX;
            this.posY = posY;
        }

        public int posX;
        public int posY;
        public string name;

        public bool haveTray;
        public bool haveForksSpoons;
        public bool haveDrink;
        public bool haveDishes = false;

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

        public void MoveTo(int destX, int destY)
        {
            if (skipSteps == 0)
            {
                if (posY > destY)
                {
                    MoveTop();
                }
                if (posX > destX)
                {
                    MoveLeft();
                }
                if (posY < destY)
                {
                    MoveBottom();
                }
                if (posX < destX)
                {
                    MoveRight();
                }
            }
            else
            {
                skipSteps--;
            }

        }

        public void takeDishes()
        {
            skipSteps = 5;//200
        }
    }
}
