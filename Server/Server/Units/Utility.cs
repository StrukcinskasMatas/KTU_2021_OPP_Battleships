using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class Utility : Unit
    {
        private int xLenght;
        private int yLenght;

        public Utility(int x, int y)
        {
            xLenght = x;
            yLenght = y;
        }

        public override string GetSizeString()
        {
            return String.Format("{0}x{1}", xLenght, yLenght);
        }

        public override string GetUnitType()
        {
            return "Utility";
        }

        public override int GetLenght()
        {
            return xLenght;
        }

        public override char GetUnitTypeSymbol()
        {
            return 'U';
        }
    }
}
