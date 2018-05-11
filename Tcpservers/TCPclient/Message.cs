using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPclient
{
    class Message
    {
        public static byte[] GetBytes(string data)
        {
            byte[] databytes = Encoding.UTF8.GetBytes(data);
            int bytesLength = databytes.Length;

            byte[] lengthBytes = BitConverter.GetBytes(bytesLength);
            byte[] newBytes = lengthBytes.Concat(databytes).ToArray();

            return newBytes;
        }
    }
}
