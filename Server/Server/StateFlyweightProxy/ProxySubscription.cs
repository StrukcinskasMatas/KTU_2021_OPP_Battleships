using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StateFlyweightProxy
{
    class ProxySubscription : ISubscribe
    {
        private ISubscribe sub;
        private Player player;
        private Grid grid;
        public ProxySubscription(Player player, Grid grid)
        {
            this.player = player;
            this.grid = grid;
        }
        public void SubscribeToMapChanges()
        {
            if (player.role.ToUpper() == "A")
            {
                sub = new RealSubscription(player, grid);
                Console.WriteLine("Subscribe Proxy makes call to the RealSub");
                sub.SubscribeToMapChanges();
            }
            else {
                Console.WriteLine("Subscribe proxy says 'You don't have permission to perform this action'");
            }
        }
    }
}
