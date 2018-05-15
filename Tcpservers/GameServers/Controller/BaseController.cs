using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServers.Controller
{
    abstract class BaseController
    {
        private RequestCode requestCode = RequestCode.None;

        public virtual void DefaultHandle() { }
    }
}
