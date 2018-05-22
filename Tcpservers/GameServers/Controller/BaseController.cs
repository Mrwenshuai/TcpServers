using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
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
        /// <summary>
        /// 当actioncode未指定的时候，执行的function
        /// </summary>
        /// <param name="data">客户端发送过来的数据，返回值为返回给客户端的信息</param>
        /// <returns></returns>
        public virtual string DefaultHandle(string data) { return null; }


        public virtual void DefaultHandle(string data , Client client,Server server) { }
    }
}
