using Server.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Visitor
{
    public interface MissMethod
    {
        public double addMissChance(Unit unit);
    }
}
