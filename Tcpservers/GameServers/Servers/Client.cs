using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using CommonLib;
using MySql.Data.MySqlClient;
using GameServers.Tool;

namespace GameServers.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();
        private MySqlConnection conn;
        public Client()
        {

        }

        public Client(Socket socket, Server server)
        {
            conn = ConnHelper.Connection();//开启数据库的链接
            this.clientSocket = socket;
            this.server = server;
        }

        public void Start()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            try
            {
                int count = clientSocket.EndReceive(asyncResult);
                if (count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count, OnProcessMessage);
                //TO DO 处理接收数据
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }
        /// <summary>
        /// 作为回调函数传递
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="actionCode"></param>
        /// <param name="data"></param>
        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        public void Close()
        {
            ConnHelper.CloseConnection(conn);//关闭数据库的链接
            if (clientSocket != null)
            {
                clientSocket.Close();
            }

            server.Remove(this);
        }
        /// <summary>
        /// 客户端发送消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="data"></param>
        public void Send(RequestCode request, string data)
        {
            byte[] bytes = Message.PackData(request, data);
            clientSocket.Send(bytes);
        }
    }
}
