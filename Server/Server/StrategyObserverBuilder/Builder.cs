using Server.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public abstract class Builder
    {
        protected Tank tankUnit;
        protected Utility utilityUnit;

        public abstract Builder addParts();

        public abstract Builder addWepons();

        public Builder startNewTank(Tank newUnit) {
            this.tankUnit = newUnit;
            return this;
        }

        public Builder startNewUtility(Utility newUnit)
        {
            this.utilityUnit = newUnit;
            return this;
        }

        public Tank getBuildableTank() {
            return this.tankUnit;
        }
        public Utility getBuildableUtility()
        {
            return this.utilityUnit;
        }

    }
}
