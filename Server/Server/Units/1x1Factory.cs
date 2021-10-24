using Server.StrategyObserverBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class _1x1Factory : AbstractFactory
    {
        public override Tank CreateTank()
        {
            Shield shield = new Shield("Titan");
            Tank tankUnit = new Tank(1, 1, shield);
            Builder builder = new TankBuilder();
            return builder.startNewTank(tankUnit).addParts().addWepons().getBuildableTank();
            //return new Tank(1, 1);
        }

        public override Utility CreateUtility()
        {
            Shield shield = new Shield("Metal");
            Utility utilityUnit = new Utility(1, 1, shield);
            Builder builder = new UtilityBuilder();
            return builder.startNewUtility(utilityUnit).addParts().addWepons().getBuildableUtility();
        }
    }
}
