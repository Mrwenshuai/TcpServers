using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServers.Tool
{
    class ConnHelper
    {
        public const string CONNECTIONINFO = "Database=game01;datasource=127.0.0.1;port=3306;sslmode=None;user=root;pwd=Wenshuai88.;";
        /// <summary>
        /// 开启数据库链接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONINFO);

            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库打开失败 ： " + e);
                return null;
            }
        }
        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
            else
            {
                Console.WriteLine("要关闭的数据库链接为空。");
            }
        }
    }
}
