using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameServers.Server
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        public Client()
        {

        }

        public Client(Socket socket, Server server)
        {
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
                msg.ReadMessage(count);
                //TO DO 处理接收数据
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        public void Close()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
            }

            server.Remove(this);
        }
    }
}
