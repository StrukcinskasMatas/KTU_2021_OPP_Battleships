using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Adapter
{
    public class AdapterDead : StatusAlive
    {
        StatusDead adaptee;
        public AdapterDead(StatusDead adaptee)
        {
            this.adaptee = adaptee;
        }
        public override string statusAlive()
        {
            return adaptee.statusDead();
        }
    }
}
