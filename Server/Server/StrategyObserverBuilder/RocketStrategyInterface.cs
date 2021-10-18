using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public interface RocketStrategyInterface
    {
        public List<Coordinates> CalculateExplosionArea(int x, int y);
    }
}
