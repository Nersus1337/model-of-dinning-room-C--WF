using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dining_room_WinForms
{
    public class Table
    {
        public Table(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            empty = true;
        }

        public int posX;
        public int posY;
        public bool empty;
    }
}
