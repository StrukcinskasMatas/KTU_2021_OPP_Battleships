using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.COR
{
    class AttackHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "A")
            {
                return $"Attack enemy's territory: (example input: \"1 2\")";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
