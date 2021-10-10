using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class _1x1Battleship : Battleship
    {
        public override AbstractFactory GetAbstractFactory()
        {
            return new _1x1Factory();
        }

        public override string GetSizeString()
        {
            return String.Format("1x1");
        }
    }
}
