using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Composite
{
    public class Fleet : GroupedUnit
    {
        protected List<GroupedUnit> children = new List<GroupedUnit>();

        public override void Add(GroupedUnit unit)
        {
            this.children.Add(unit);
        }

        public override void Remove(GroupedUnit unit)
        {
            this.children.Remove(unit);
        }

        public override void turnOnSirens() 
        {
            foreach (var child in this.children)
            {
                child.turnOnSirens();
            }
        }

        public override bool isUnit()
        {
            return false;
        }
    }
}
