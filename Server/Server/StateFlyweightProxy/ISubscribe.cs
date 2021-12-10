using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server.StateFlyweightProxy
{
    public interface ISubscribe
    {
        void SubscribeToMapChanges();
    }
}
