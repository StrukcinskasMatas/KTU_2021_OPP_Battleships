using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interpreter
{
    class UpgradeExpression : AbstractExpression
    {
        public override string Expand() { return "U"; }
        public override string FullName() { return "Upgrade"; }

    }
}
