using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Units;

namespace Server.StrategyObserverBuilder
{
    public interface IObserver
    {
        public void Update(Cell[,] mapGrid);
    }
}