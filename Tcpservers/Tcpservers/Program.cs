using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Tcpservers
{
    class Program
    {
        static void Main(string[] args)
        {
            StartAsyncServer();
            Console.ReadKey();
        }

        static void StartAsyncServer()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("192.168.2.100");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 13141);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);

            serverSocket.BeginAccept(BeginAcceptAsync, serverSocket);
        }
        /// <summary>
        /// 开始异步，向客户端发送一条msg消息
        /// </summary>
        /// <param name="ar"></param>
        static void BeginAcceptAsync(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);

            string msgStr = " hello client , 你好！ ";
            byte[] data = Encoding.UTF8.GetBytes(msgStr);
            clientSocket.Send(data);
            //开始接收消息
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ServerReceiveCallBackMsg, clientSocket);
            serverSocket.BeginAccept(BeginAcceptAsync, serverSocket);
        }

        static byte[] dataBuffer = new byte[1024];
        static Message msg = new Message();

        /// <summary>
        /// 开始接收客户端消息回调
        /// </summary>
        /// <param name="ar"></param>
        static void ServerReceiveCallBackMsg(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);//获取读取到的消息的长度

                if (count == 0)
                {
                    clientSocket.Close();
                    return;
                }

                msg.AddCount(count);//更新message中startindex的长度
                //string msgStr = Encoding.UTF8.GetString(dataBuffer, 0, count);
                //Console.WriteLine("Receive msg form client : " + msgStr);
                //clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ServerReceiveCallBackMsg, clientSocket);
                msg.ReadMessage();

                clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ServerReceiveCallBackMsg, clientSocket);

            }
            catch (Exception e)
            {
                Console.WriteLine("SERVER ERROR INFO ::   " + e);

                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
        }
    }
}
