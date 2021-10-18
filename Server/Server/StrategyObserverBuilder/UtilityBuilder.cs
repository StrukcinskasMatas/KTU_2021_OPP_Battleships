using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Units;

namespace Server.StrategyObserverBuilder
{
    public class UtilityBuilder : Builder
    {
        public void addShields()
        {
            utilityUnit.setBody("shields");
        }
        public void addSupplies()
        {
            utilityUnit.setWeapon("supplies");
        }
        public override Builder addParts()
        {
            addShields();
            return this;
        }

        public override Builder addWepons()
        {
            addSupplies();
            return this;
        }
    }
}
