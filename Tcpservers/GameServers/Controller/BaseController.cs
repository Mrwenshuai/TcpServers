using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServers.Servers;

namespace GameServers.Controller
{
    abstract class BaseController
    {
        private RequestCode requestCode = RequestCode.None;

        public RequestCode RequestCode
        {
            get { return requestCode; }
        }

        public virtual void DefaultHandle(string data , Client client,Server server) { }
    }
}
