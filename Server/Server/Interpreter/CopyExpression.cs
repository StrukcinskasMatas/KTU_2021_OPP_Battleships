using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interpreter
{
    class CopyExpression: AbstractExpression
    {
        public override string Expand() { return "C"; }
        public override string FullName() { return "Copy"; }

    }
}
