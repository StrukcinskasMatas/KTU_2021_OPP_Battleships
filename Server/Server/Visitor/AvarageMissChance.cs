using Server.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Visitor
{
    public class AvarageMissChance : MissMethod
    {
        double baseChance = 5;
        public double chance()
        {
            return baseChance;
        }
        public double addMissChance(Unit unit)
        {
            return unit.calculateMiss(this);
        }
    }
}