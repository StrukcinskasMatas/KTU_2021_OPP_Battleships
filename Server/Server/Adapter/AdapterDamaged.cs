using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Adapter
{
    public class AdapterDamaged : StatusAlive
    {
        StatusDamaged adaptee;
        public AdapterDamaged(StatusDamaged adaptee)
        {
            this.adaptee = adaptee;
        }
        public override string statusAlive()
        {
            return "Adapter damaged | " + adaptee.statusDamaged();
        }
    }
}
