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

namespace WindowsFormskaoshengsystem
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            /*
             * 读取数据库中的学号、考场、座位号，并将其设置为相应下拉列表框的可选项！
             */ 
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
            SqlDataReader sdr1 = iBaby.DBOperation.getReader("select distinct 考场 from student");
            SqlDataReader sdr2 = iBaby.DBOperation.getReader("select distinct 座位号 from student");
            SqlDataReader sdr3 = iBaby.DBOperation.getReader("select 学号 from student");
            while (true)
            {
                if (sdr1.Read())
                {
                    comboBox2.Items.Add(sdr1.GetString(0));
                }
                else
                {
                    break;
                }
            }
            while(true)
            {
                if (sdr2.Read())
                {
                    comboBox3.Items.Add(sdr2.GetString(0));
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                if (sdr3.Read())
                {
                    comboBox4.Items.Add(sdr3.GetString(0));
                }
                else
                {
                    break;
                }
            }
            sdr1.Close();
            sdr2.Close();
            sdr3.Close();
        }
        public static string str1, str2, str3, str4;
        public static bool flag = false;
        private void btn_OK_Click(object sender, EventArgs e)
        {
            /*
             * 使用static的字符串来将子窗体的信息传递到主窗体！
             */
            flag = true;
            str1 = comboBox4.Text;
            str2 = comboBox1.Text;
            str3 = comboBox2.Text;
            str4 = comboBox3.Text;
            this.Close();
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
           
        }
    }
}
