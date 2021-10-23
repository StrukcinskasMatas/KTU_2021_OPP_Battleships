using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public abstract class Unit: ICloneable
    {
        public abstract string GetUnitType();
        public abstract char GetUnitTypeSymbol();
        public abstract string GetSizeString();
        public abstract int GetLenght();
        public abstract object Clone();
        public abstract string getConfiguration();
        public abstract string Operation();
    }
}
