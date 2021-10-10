using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public abstract class Battleship
    {
        public abstract AbstractFactory GetAbstractFactory();
        public abstract string GetSizeString();
    }
}
