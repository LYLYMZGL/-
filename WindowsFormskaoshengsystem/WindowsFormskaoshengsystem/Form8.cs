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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
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
            iBaby.DBOperation.BindDB(dataGridView1,"select *  from student","student");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = "";
            string str1_1 = "select * from student where 学号='" + comboBox1.Text + "'";
            string str1_2 = "select * from student where 姓名='" + comboBox2.Text + "'";
            string str1_3 = "select * from student where 性别='" + comboBox3.Text + "'";
            string str1_4 = "select * from student where 考场='" + comboBox4.Text + "'";
            string str1_5 = "select * from student where 座位号='" + comboBox5.Text + "'";
            if (comboBox1.Text != "")
            {
                if (str1 != "")
                {
                    str1 += "AND 学号='" + comboBox1.Text + "'";
                }
                else
                {
                    str1 = str1_1;
                }
            }
            if (comboBox2.Text != "")
            {
                if (str1 != "")
                {
                    str1 += "AND 姓名='" + comboBox2.Text + "'";
                }
                else
                {
                    str1 = str1_2;
                }
            }
            if (comboBox3.Text != "")
            {
                if (str1 != "")
                {
                    str1 += "AND 性别='" + comboBox3.Text + "'";
                }
                else
                {
                    str1 = str1_3;
                }
            }
            if (comboBox4.Text != "")
            {
                if (str1 != "")
                {
                    str1 += "AND 考场='" + comboBox4.Text + "'";
                }
                else
                {
                    str1 = str1_4;
                }
            }
            if (comboBox5.Text != "")
            {
                if (str1 != "")
                {
                    str1 += "AND 座位号='" + comboBox5.Text + "'";
                }
                else
                {
                    str1 = str1_5;
                }
            }
            iBaby.DBOperation.BindDB(dataGridView1,str1,"student");
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
