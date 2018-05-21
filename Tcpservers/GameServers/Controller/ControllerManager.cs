using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServers.Servers;

namespace GameServers.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;

        public void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);

        }

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);

            if (isGet == false)
            {
                Console.WriteLine("要请求的requestCode ： " + requestCode + "所对应的controller为空，"); return;
            }

            string methodName = Enum.GetName(typeof(ActionCode), actionCode);

            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[Waring] controller[ " + controller.GetType() + " ]中没有对应的处理方法 ：  " + methodName);
            }
            object[] parameters = new object[] { data, client, server };
            object o = mi.Invoke(controller, parameters);

            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }

            server.SendResponse(client, requestCode, o as string);
        }
    }
}
