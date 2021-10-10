using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public abstract class AbstractFactory
    {
        public abstract Tank CreateTank();
        public abstract Utility CreateUtility();
    }
}
