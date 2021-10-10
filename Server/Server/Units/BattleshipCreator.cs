using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class BattleshipCreator : Creator
    {
        public override Battleship CreateBattleship(int type)
        {
            switch (type)
            {
                case 1:
                    return new _1x1Battleship();
                case 2:
                    return new _2x1Battleship();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
