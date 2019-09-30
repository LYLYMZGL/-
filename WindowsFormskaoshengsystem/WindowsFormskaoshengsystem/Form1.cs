using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormskaoshengsystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /*
             * 为下拉文本框添加可选项，使登录操作更加便捷、信息不容易出错
             */
            comboBox1.Items.Add("(local)\\MSSQLSERVERR");  
            comboBox2.Items.Add("sa_test");
            comboBox3.Items.Add("sa");
            comboBox4.Items.Add("123");
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            /*
             * 将数据库连接字符串设置成可以输入的，这样将代码移植到别的电脑上时，就可以不用修改数据库连接字符串
             */
            string path = @"数据库连接信息.txt"; //文件所在路径（使用了相对路径）
            string s1, s2, s3, s4;          //使用字符串用来保存comboBox中的信息
            s1 = comboBox1.Text;
            s2 = comboBox2.Text;
            s3 = comboBox3.Text;
            s4 = comboBox4.Text;
            StreamWriter sw = new StreamWriter(path, false);//（文件所在路径，bool值）true表示追加，不覆盖之前的信息
            //如果此值为false，则创建一个新文件，如果存在原文件，则覆盖。
            //如果此值为true，则打开文件保留原来数据，如果找不到文件，则创建新文件。
            sw.WriteLine(s1);  //往文件中写入数据库服务器名
            sw.WriteLine(s2);  //往文件中写入数据库名
            sw.WriteLine(s3);  //往文件中写入数据库登录名
            sw.WriteLine(s4);  //往文件中写入与数据库登录名相匹配的密码
            sw.Close();        //关闭流。若不关闭，则会抛出异常
            Form2 f2 = new Form2();  //打开登录界面
            f2.ShowDialog();
        }
    }
}
