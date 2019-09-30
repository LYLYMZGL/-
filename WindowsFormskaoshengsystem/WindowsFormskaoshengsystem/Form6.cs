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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
            
        }
        public static string str1, str2, str3, str4, str5;
        public static bool flag = false;
        private void btn_OK_Click(object sender, EventArgs e)
        {
            flag = true;
            str1 = comboBox1.Text;
            str2 = comboBox2.Text;
            str3 = comboBox3.Text;
            str4 = comboBox4.Text;
            str5 = comboBox5.Text;
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            SqlDataReader sdr1 = iBaby.DBOperation.getReader("select 学号 from student");
            SqlDataReader sdr2 = iBaby.DBOperation.getReader("select 姓名 from student");
            SqlDataReader sdr3 = iBaby.DBOperation.getReader("select distinct 性别 from student");
            SqlDataReader sdr4 = iBaby.DBOperation.getReader("select distinct 考场 from student");
            SqlDataReader sdr5 = iBaby.DBOperation.getReader("select distinct 座位号 from student");
            while (true)
            {
                if (sdr1.Read())
                {
                    comboBox1.Items.Add(sdr1.GetString(0));
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                if (sdr2.Read())
                {
                    comboBox2.Items.Add(sdr2.GetString(0));
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
                    comboBox3.Items.Add(sdr3.GetString(0));
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                if (sdr4.Read())
                {
                    comboBox4.Items.Add(sdr4.GetString(0));
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                if (sdr5.Read())
                {
                    comboBox5.Items.Add(sdr5.GetString(0));
                }
                else
                {
                    break;
                }
            }
        }
    }
}
