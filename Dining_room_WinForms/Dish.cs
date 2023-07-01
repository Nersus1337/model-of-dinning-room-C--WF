using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dining_room_WinForms
{
    public class Dish
    {

        public Dish(int posX, int posY, string name, int cost, int taste, int count) 
        {
            this.posX = posX;
            this.posY = posY;
            this.name = name;
            this.cost = cost;
            this.taste = taste;
            this.count = count;
        }

        public int posX;
        public int posY;
        public string name;
        public int cost;
        public int taste;
        public int count;


    }
}
