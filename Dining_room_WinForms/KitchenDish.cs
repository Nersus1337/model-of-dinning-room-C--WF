using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dining_room_WinForms
{
    public class CitchenDish
    {

        public CitchenDish(int posX, int posY, string name, int count)
        {
            this.posX = posX;
            this.posY = posY;
            this.name = name;
            this.count = count;
        }

        public int posX;
        public int posY;
        public string name;
        public int count;


    }
}
