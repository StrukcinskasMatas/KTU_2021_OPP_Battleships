using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.StrategyObserverBuilder;

namespace Server.StateFlyweightProxy
{
    class RealSubscription : ISubscribe
    {
        private Player player;
        private Grid grid;
        public RealSubscription(Player player, Grid grid)
        {
            this.player = player;
            this.grid = grid;
        }
        public void SubscribeToMapChanges()
        {
            Console.WriteLine("Subscribing");
            grid.Attach(player);
        }
    }
}
