using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public abstract class AbstractFactory
    {
        public Dictionary<char, Tank> tanks = new Dictionary<char, Tank>();

        public abstract Tank CreateTank(char color);
        public abstract Utility CreateUtility();
    }
}
