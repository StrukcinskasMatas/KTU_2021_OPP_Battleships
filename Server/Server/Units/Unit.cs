using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.StrategyObserverBuilder;

namespace Server.Units
{
    public abstract class Unit: ICloneable
    {
        public abstract string GetUnitType();
        public abstract char GetUnitTypeSymbol();
        public abstract string GetSizeString();
        public abstract int GetLenght();
        public abstract object Clone();
        public abstract object DeepClone();
        public abstract string getConfiguration();
        public abstract string Operation();
        public abstract string GetUnitInfo();
        //public void Update(Cell[,] mapGrid)
        //{
        //    Console.WriteLine("Be aware unit spawned at random location in the map!! Message from: " + GetUnitType());
        //}
    }
}
