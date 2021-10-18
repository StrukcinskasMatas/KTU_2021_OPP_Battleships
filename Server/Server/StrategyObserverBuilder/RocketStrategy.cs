using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public class SmallExplosion : RocketStrategyInterface
    {
        public List<Coordinates> CalculateExplosionArea(int x, int y)
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(x, y));
            coordinates.Add(new Coordinates(x + 1, y));
            coordinates.Add(new Coordinates(x, y + 1));
            return coordinates;
        }
    }
    public class MediumExplosion : RocketStrategyInterface
    {
        public List<Coordinates> CalculateExplosionArea(int x, int y)
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(x, y));
            coordinates.Add(new Coordinates(x + 1, y));
            coordinates.Add(new Coordinates(x, y + 1));
            coordinates.Add(new Coordinates(x + 2, y));
            coordinates.Add(new Coordinates(x, y + 2));
            return coordinates;
        }
    }
    public class BigExplosion : RocketStrategyInterface
    {
        public List<Coordinates> CalculateExplosionArea(int x, int y)
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(x, y));
            coordinates.Add(new Coordinates(x + 1, y));
            coordinates.Add(new Coordinates(x, y + 1));
            coordinates.Add(new Coordinates(x + 2, y));
            coordinates.Add(new Coordinates(x, y + 2));
            coordinates.Add(new Coordinates(x + 3, y));
            coordinates.Add(new Coordinates(x, y + 3));
            return coordinates;
        }
    }
}