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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
            SqlDataReader sdr1 = iBaby.DBOperation.getReader("select 学号 from student");
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
        }
        public static string  str1,str2, str3, str4, str5;
        public static bool flag = false;
        private void btn_OK_Click(object sender, EventArgs e)
        {
            flag = true;
            str1 = comboBox2.Text;
            str2 = textBox2.Text;
            str3 = comboBox1.Text;
            str4 = textBox3.Text;
            str5 = textBox4.Text;
            this.Close();
        }
    }
}
