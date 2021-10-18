using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public class Coordinates
    {
        private int x_coordinate;
        private int y_coordinate;

        public Coordinates(int x_coordinate, int y_coordinate) {
            this.x_coordinate = x_coordinate;
            this.y_coordinate = y_coordinate;
        }

        public Tuple<int, int> GetCoordinates() {
            return Tuple.Create(this.x_coordinate, this.y_coordinate);
        }
    }
}
