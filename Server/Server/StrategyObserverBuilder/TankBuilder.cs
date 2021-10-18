using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public class TankBuilder : Builder
    {
        public void addArrmor() {
            tankUnit.setBody("arrmor");
        }
        public void addMachineGuns() {
            tankUnit.setWeapon("machineGuns");
        }
        public override Builder addParts()
        {
            addArrmor();
            return this;
        }

        public override Builder addWepons()
        {
            addMachineGuns();
            return this;
        }
    }
}
