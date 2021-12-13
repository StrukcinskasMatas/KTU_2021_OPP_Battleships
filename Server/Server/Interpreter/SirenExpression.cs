using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interpreter
{
    class SirenExpression : AbstractExpression
    {
        public override string Expand() { return "S"; }
        public override string FullName() { return "Siren"; }

    }
}
