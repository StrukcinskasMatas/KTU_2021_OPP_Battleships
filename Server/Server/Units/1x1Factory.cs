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
        private _1x1Factory() { }
        private static _1x1Factory instance = null;
        public static _1x1Factory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new _1x1Factory();
                }
                return instance;
            }
        }
        public override Tank CreateTank(char color)
        {
            Tank tank = null;
            if (tanks.ContainsKey(color) == false)
            {
                Shield shield = new Shield("Titan");
                Tank tankUnit = new Tank(1, 1, shield, color);
                Builder builder = new TankBuilder();
                tank = builder.startNewTank(tankUnit).addParts().addWepons().getBuildableTank();
                tanks.Add(color, tank);
                Console.WriteLine("FLIGHTWEIGHT CREATED NEW OBJECT");
            }
            else {
                tank = tanks[color];
                Console.WriteLine("FLIGHTWEIGHT REUSED OBJECT FROM LIST");
            }

            return tank;
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
