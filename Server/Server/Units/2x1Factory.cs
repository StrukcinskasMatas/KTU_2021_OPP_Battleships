using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class _2x1Factory : AbstractFactory
    {
        public override Tank CreateTank()
        {
            Shield shield = new Shield("Titan");
            return new Tank(2, 1, shield);
        }

        public override Utility CreateUtility()
        {
            Shield shield = new Shield("Metal");
            return new Utility(2, 1, shield);
        }
    }
}
