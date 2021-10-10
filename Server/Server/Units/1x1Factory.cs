using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class _1x1Factory : AbstractFactory
    {
        public override Tank CreateTank()
        {
            return new Tank(1, 1);
        }

        public override Utility CreateUtility()
        {
            return new Utility(1, 1);
        }
    }
}
