using Server.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Decorator
{
    abstract class Decorator : Unit
    {
        protected Unit wrapee;
        public Decorator (Unit component)
        {
            wrapee = component;
        }
        public void SetComponent(Unit component)
        {
            this.wrapee = component;
        }
        public override string Operation()
        {
            if (this.wrapee != null)
            {
                return this.wrapee.Operation();
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
