using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Iterator
{
    public class ShipSize
    {
        private int size;

        public ShipSize(int size)
        {
            this.size = size;
        }

        public int GetShipSize()
        {
            return size;
        }
    }
}
