using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySqlOperationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Database=test007;Data Source=127.0.0.1;port=3306;sslmode=None;User Id=root;Password=Wenshuai88.;";
            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            #region 查询数据库的方式
            //MySqlCommand cmd = new MySqlCommand("select * from user", conn);
            //MySqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    /*reader.Read()*/ //调用一次 读取一行reader.HasRows
            //    string name = reader.GetString("username");
            //    string password = reader.GetString("password");

            //    Console.WriteLine(name + " >> " + password);

            //}

            //reader.Close();
            #endregion

            #region 数据插入
            //string username = "zzz";
            //string pwd = "kkk;delete from user;";

            ////MySqlCommand cmd = new MySqlCommand("insert into user set username ='" + username + "'" + ",password = '" + pwd + "'", conn);

            ////为了防止发生sql注入
            //MySqlCommand cmd = new MySqlCommand("insert into user set username=@un,password=@pwd", conn);
            //cmd.Parameters.AddWithValue("un", username);
            //cmd.Parameters.AddWithValue("pwd", pwd);

            //cmd.ExecuteNonQuery();//执行
            #endregion

            #region 数据的删除和更新
            //MySqlCommand cmd = new MySqlCommand("delete from user where id=@id", conn);
            //cmd.Parameters.AddWithValue("id", 2);

            //cmd.ExecuteNonQuery();

            //MySqlCommand cmds = new MySqlCommand("update user set password = @pwd where id = 3", conn);
            //cmds.Parameters.AddWithValue("pwd", 12312);
            //cmds.ExecuteNonQuery();

            #endregion
            conn.Close();

            Console.ReadKey();
        }
    }
}
