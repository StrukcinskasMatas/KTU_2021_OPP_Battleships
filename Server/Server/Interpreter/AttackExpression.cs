using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interpreter
{
    class AttackExpression: AbstractExpression
    {
        public override string Expand() { return "A"; }
        public override string FullName() { return "Attack"; }
    }
}
