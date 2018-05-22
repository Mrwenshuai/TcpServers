using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using System.Reflection;
using GameServers.Servers;

namespace GameServers.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();

        private Server server;
        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        public void InitController()
        {
            //添加到字典中
            DefaultController defaultController = new DefaultController();

            controllerDict.Add(defaultController.RequestCode, defaultController);
        }

        /// <summary>
        /// 当requestcode没有指定的时候 调用该function
        /// </summary>
        /// <param name="request"></param>
        /// <param name="actionCode"></param>
        public void HandleRequest(RequestCode request, ActionCode actionCode, string data, Client client)
        {
            BaseController baseController;

            bool isGet = controllerDict.TryGetValue(request, out baseController);
            if (isGet == false)
            {
                Console.WriteLine("获取" + request + "所对应的Controller失败"); return;
            }

            string methodName = Enum.GetName(typeof(ActionCode), actionCode);//获取方法名
            MethodInfo mi = baseController.GetType().GetMethod(methodName);//得到方法信息
            if (mi == null)
            {
                Console.WriteLine("Waring : 在controller：[" + baseController.GetType() + "]中没有对应的处理方法: " + methodName + "，未获得到方法名信息。");
                return;
            }
            object[] parameters = new object[] { data, client, server };
            object o = mi.Invoke(baseController, parameters);//执行方法，并传入参数

            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }

            server.SendResponse(client, request, o as string);

        }

    }
}
