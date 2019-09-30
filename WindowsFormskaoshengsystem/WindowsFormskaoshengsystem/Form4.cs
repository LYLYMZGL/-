using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormskaoshengsystem
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public static string str1, str2, str3, str4, str5;
        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
            //将窗体的CanselButton属性设置为当前按钮
        }
        public static bool flag = false;
        private void btn_OK_Click(object sender, EventArgs e)
        {
            /*
             * 使用static的字符串来将子窗体的信息传递到主窗体！
             */ 
            flag = true;  //判断是否按下“确定”按钮
            //此处不需要写当管理员没有输入全部信息的情况，因为会在主界面中抛出异常！
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                str1 = textBox1.Text;
                str2 = textBox2.Text;
                str3 = comboBox1.Text;
                str4 = textBox3.Text;
                str5 = textBox4.Text;
            }
            this.Close();
        }
    }
}
