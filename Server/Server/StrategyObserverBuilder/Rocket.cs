using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public class Rocket
    {
        public RocketStrategyInterface strategy { get; set; }

        public Rocket() { }

        public Rocket(RocketStrategyInterface strategy)
        {
            this.strategy = strategy;
        }

        public List<Coordinates> explotionDamageArea(int x, int y)
        {
            return strategy.CalculateExplosionArea(x, y);
        }
    }
}
