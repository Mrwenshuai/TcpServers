using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;

namespace GameServers.Servers
{
    class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;

        public byte[] Data
        {
            get { return data; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }

        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }

        //public void AddCount(int c)
        //{
        //    startIndex += c;
        //}
        /// <summary>
        /// 解析读取到的数据
        /// </summary>
        public void ReadMessage(int dataAmount, Action<RequestCode, ActionCode, string> processDataCallBack)
        {
            startIndex += dataAmount;
            while (true)
            {
                if (startIndex <= 4)//长度不够，直接返回
                {
                    return;
                }

                int count = BitConverter.ToInt32(data, 0);
                if (startIndex - 4 >= count)
                {
                    //string s = Encoding.UTF8.GetString(data, 4, count);
                    //Console.WriteLine("解析出一条数据： " + s);

                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);//解析出来requestcode,从4的位置开始解析。 
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);//而解析actioncode则从8的位置开始解析。

                    string s = Encoding.UTF8.GetString(data, 12, count - 8);

                    processDataCallBack(requestCode, actionCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }

        public static byte[] PackData(RequestCode request, string data)
        {
            byte[] requestBytes = BitConverter.GetBytes((int)request);//数据的第一段
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);//数据的第二段

            int amount = requestBytes.Length + dataBytes.Length;

            byte[] dataAmoutBytes = BitConverter.GetBytes(amount);//数据的第三段

            return (byte[])dataAmoutBytes.Concat(requestBytes).Concat(dataBytes);
        }
    }
}
