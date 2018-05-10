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
            //


        }

        static void BeginAcceptAsync(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);

            string msg = " hello client , 你好！ ";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);

            clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ServerReceiveMsg, clientSocket);
            serverSocket.BeginAccept(BeginAcceptAsync, serverSocket);
        }

        static byte[] dataBuffer = new byte[1024];
        static void ServerReceiveMsg(IAsyncResult ar)
        {
            Socket clientSocket = ar.AsyncState as Socket;
            int count = clientSocket.EndReceive(ar);
            string msg = Encoding.UTF8.GetString(dataBuffer, 0, count);

            Console.WriteLine("Receive msg form client : " + msg);
            clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ServerReceiveMsg, clientSocket);
        }
    }
}
