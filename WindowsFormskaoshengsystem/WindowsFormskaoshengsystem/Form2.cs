using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormskaoshengsystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btn_determine_Click(object sender, EventArgs e)
        {
            /*
             * 使用SqlDataReader的Read()方法来读取数据库，判断users表格中是否有文本框中输入账号密码。若存在，则登录成功；反之，则失败。
             * 登录分为用户登录和管理员登录，管理员有增删查改考生信息的权限，而用户只具有查询考生信息的功能。
             */
            string str1 = "select * from users where 账号='" + textBox1.Text + "'and 密码='" + textBox2.Text + "'";
            SqlDataReader sqr = iBaby.DBOperation.getReader(str1);
            if (sqr.Read())
            {
                if (textBox1.Text=="admin"&&textBox2.Text=="666666")  //若输入的是管理员账号密码，则登录管理员账号
                {
                    MessageBox.Show("管理员登录成功！");
                    Form3 f3 = new Form3();
                    f3.ShowDialog();
                }
                else                                                 //若输入的是用户账号密码，则登录用户账号
                {
                    MessageBox.Show("普通用户登录成功！");
                    Form8 f8 = new Form8();
                    f8.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("账号或密码错误！");
            }
            sqr.Close();
        }

        private void btn_cansel_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            /*
             * 在注册用户时，限制了账号只能为9位数的数字，密码只能为6位数的数字。若输入的格式符合要求，则将想要注册的账号密码插入到
             * 数据库中，插入操作完成后再使用SqlDataReader的Read()方法读取数据库。若读取到想注册的账号密码，则显示注册成功，否则，
             * 显示注册失败。
             */
            label4.Text = "";
            label5.Text = "";
            Regex reg1 = new Regex("^\\d{9}$");
            Regex reg2 = new Regex("^\\d{6}$");
            if (!reg1.IsMatch(textBox1.Text))
            {
                label4.Text = "账号只能为9位数的数字";
            }
            if (!reg2.IsMatch(textBox2.Text))
            {
                label5.Text = "密码只能为6位数的数字";
            }
            if(reg1.IsMatch(textBox1.Text)&&reg2.IsMatch(textBox2.Text))
            {
                string str1 = "insert into users values('" + textBox1.Text + "','" + textBox2.Text + "')";
                iBaby.DBOperation.exesql(str1);
                SqlDataReader sdr = iBaby.DBOperation.getReader("select * from users");
                if (sdr.Read())
                {
                    MessageBox.Show("注册成功！");
                }
                else
                {
                    MessageBox.Show("注册失败！");
                }
            }
        }
    }
}
