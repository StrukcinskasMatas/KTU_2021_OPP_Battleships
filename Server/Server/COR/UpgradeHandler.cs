using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.COR
{
    class UpgradeHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "U")
            {
                return $"Select ship to upgrade: (example input: \"1 2\")";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
