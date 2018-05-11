using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCPclient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.2.100"), 13141);
            clientSocket.Connect(ipEndPoint);

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data, 0, count);

            Console.WriteLine(msg + "  ---->> msg");
            //while (true)
            //{
            //    string s = Console.ReadLine();
            //    if (s == "q")
            //    {
            //        clientSocket.Close();
            //    }
            //    clientSocket.Send(Encoding.UTF8.GetBytes(s));
            //}

            for (int i = 0; i < 100; i++)
            {
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }

            //string s = @"lfadjflfladkjfladjfl";
            //clientSocket.Send(Encoding.UTF8.GetBytes(s));

            Console.ReadKey();
            clientSocket.Close();
        }
    }
}
