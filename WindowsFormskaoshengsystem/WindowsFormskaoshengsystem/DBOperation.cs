using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;// Directory


namespace iBaby
{
    class DBOperation
    {
       // static String currentdir = Directory.GetCurrentDirectory();
        //static String constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + currentdir + @"\susheguanli_data.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        //static String constr = @"Data Source=.\SQLEXPRES;AttachDbFilename="+currentdir+@"\Data\iBaby_Data.MDF;Integrated Security=True;Connect Timeout=30;User Instance=True";
        //static String constr = @"Data Source=(local)\MSSQLSERVERR;Initial Catalog=sa_test1;User ID=sa;password=123;";
        public static string A()  //方法必须声明为static的，否则下面的constr无法直接调用
        {
            string path = @"数据库连接信息.txt";
            string[] content = File.ReadAllLines(path, Encoding.Default);//使用string数组来存储从文件中读取到的信息
            //读取path路径下的文件内容，若不加“Encoding.Default”则会中文乱码
            //Encoding.Default（编码.默认）（C#中的默认编码方式为Unicode）（记事本的默认编码方式为ANSI）
            string constr1 = @"Data Source='" + content[0] + "';Initial Catalog='" + content[1] + "';User ID='" + content[2] + "';password='" + content[3] + "';";
            return constr1;
        }
        /*
         * 读取“数据库连接信息.txt”文件里的数据库连接字符串信息，动态生成数据库连接字符串
         */
        static String constr = A();  //静态方法可以直接用类.函数调用，不需要再声明对象来调用（在本类中可以直接使用方法名调用）
        //绑定DataGridView
        public static void BindDB(DataGridView dg,string strSql, string Tname)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter sda = new SqlDataAdapter(strSql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, Tname);
            dg.DataSource = ds.Tables[Tname];
        }
        //  获取数据表
        public static DataTable  GetDataTable( string strSql)
        {
            DataTable dt ;
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter sda = new SqlDataAdapter(strSql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "DataTab"); //调用SqlDataAdapter的Fill方法，并指定要填充的DataSet表
            dt = ds.Tables["DataTab"];
            //dt.Clear();  //清除控件上的内容
            return dt;
        }
       ///添加 删除 修改
        public static void exesql(String strsql)  
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();///建立连接
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = strsql;
            cmd.ExecuteNonQuery(); //通过执行update、insert、delete语句，在不使用DataSet的情况下更改数据库中的数据
            con.Close();
        }
        //根据select查询sql，返回datareader
        public static SqlDataReader getReader(string sql)
       {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();  //DataReader中的Read()方法：依次读取下一条数据
            return reader;
       }
       
    }
}
