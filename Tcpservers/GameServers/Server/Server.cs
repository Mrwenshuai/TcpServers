using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using GameServers.Controller;

namespace GameServers.Server
{
    class Server
    {
        Socket serverSocket = null;
        private IPEndPoint ipEndPoint;
        private List<Client> clientList;
        private ControllerManager controllerManager = new ControllerManager();

        public Server()
        {
        }

        public Server(string ip, int port)
        {
            SetIpAndPort(ip, port);
        }

        public void SetIpAndPort(string ip, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);

            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        private void AcceptCallBack(IAsyncResult async)
        {
            Socket clientSokcet = serverSocket.EndAccept(async);
            Client client = new Client(clientSokcet, this);
            client.Start();
            clientList.Add(client);

        }

        public void Remove(Client client)
        {
            lock (clientList)//加锁防止异步操作时出现问题，保证每次只有一个在操作list
            {
                clientList.Remove(client);
            }
        }
    }
}
