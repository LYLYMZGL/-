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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        static string constr = "select * from student order by 学号";
        private void Form3_Load(object sender, EventArgs e)
        {
            iBaby.DBOperation.BindDB(dataGridView1,constr,"student");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            /*
             * 添加功能中，输入的信息有相应的格式限制，且同一个考场的座位号不能重复。若格式输入正确，则执行添加操作，将考生信息插入到student表中，
             * 并重新绑定控件，让其显示添加后的student表。
             * 若添加时没有输入全部信息，则信息全为空，会产生异常
             */ 
            Form4 f1 = new Form4();
            f1.ShowDialog();
            if (Form4.flag == true)
            {
                try     //数据库中设置了主键，当输入的学号重复时，会抛出异常；并且当输入的数据不完整时，会显示提示。
                {
                    bool flag=false;
                    SqlDataReader sdr = iBaby.DBOperation.getReader("select 考场 from student ");
                    SqlDataReader sdr1 = iBaby.DBOperation.getReader("select 座位号 from student where 考场='"+Form4.str4+"'");
                    string str1 = "";
                    Regex reg1 = new Regex("^\\d{9}$");
                    Regex reg2 = new Regex("^\\d{4}$");
                    Regex reg3 = new Regex("^\\d{1,2}$");
                    if (!reg1.IsMatch(Form4.str1))
                    {
                        MessageBox.Show("学号格式错误（格式为：9个数字），请重新输入！");
                    }
                    if (!reg2.IsMatch(Form4.str4))
                    {
                        MessageBox.Show("考场格式错误（格式为：4个数字），请重新输入！");
                    }
                    if (!reg3.IsMatch(Form4.str5))
                    {
                        MessageBox.Show("座位号格式错误（格式为：1-2个数字），请重新输入！");
                    }
                    else
                    {
                        while (true)
                        {
                            if (sdr.Read())
                            {
                                if (sdr.GetString(0) == Form4.str4)  //判断当前文本框中输入的考场是否存在
                                {
                                    while (true)
                                    {
                                        if (sdr1.Read())
                                        {
                                            if (sdr1.GetString(0) == Form4.str5) //判断文本框中输入的座位号是否存在
                                            {
                                                flag = true;
                                                MessageBox.Show(""+Form4.str4 +" 考场的 "+ Form4.str5 +" 号座位号已经有考生了！");
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (flag == false)   //若输入的同一个考场的座位号没有重复，则执行插入语句
                        {
                            str1 = "insert into student values('" + Form4.str1 + "','" + Form4.str2 + "','" + Form4.str3 + "','" + Form4.str4 + "','" + Form4.str5 + "')";
                            iBaby.DBOperation.exesql(str1);
                        }
                    }
                    iBaby.DBOperation.BindDB(dataGridView1, constr, "student");
                    sdr.Close();
                    sdr1.Close();
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            /*
             * 删除功能可以根据学号、性别、考场、座位号来进行删除！
             * 因为输入删除条件时已经是只能选择已经存在的数据，所以无需限制格式！
             */
            MessageBox.Show("删除可以根据学号、性别、考场、座位号来删除！");
            Form5 f2 = new Form5();
            f2.ShowDialog();
            if (Form5.flag == true)
            {
                string str2 = "";
                if (Form5.str1 != "")
                    str2 = "delete from student where 学号='" + Form5.str1 + "'";
                if (Form5.str2 != "")
                    str2 = "delete from student where 性别='" + Form5.str2 + "'";
                if (Form5.str3 != "")
                    str2 = "delete from student where 考场='" + Form5.str3 + "'";
                if (Form5.str4 != "")
                    str2 = "delete from student where 座位号='" + Form5.str4 + "'";
                if (str2 != "")
                {
                    iBaby.DBOperation.exesql(str2);
                    iBaby.DBOperation.BindDB(dataGridView1, constr, "student");
                }
                else
                {
                    MessageBox.Show("没有选择删除条件！");
                }
            }
        }

        private void btn_sel_Click(object sender, EventArgs e)
        {
            /*
             * 查询功能可以但条件查询，也可以多条件查询。
             * 因为输入查询条件时已经是只能选择已经存在的数据，所以无需限制格式！
             */
            MessageBox.Show("查询可以单个条件查询，也可以多条件查询！");
            Form6 f3 = new Form6();
            f3.ShowDialog();
            if (Form6.flag == true)
            {
                string str3 = "";
                string str3_1 = "select * from student where 学号='" + Form6.str1 + "'"; ;
                string str3_2 = "select * from student where 姓名='" + Form6.str2 + "'";
                string str3_3 = "select * from student where 性别='" + Form6.str3 + "'";
                string str3_4 = "select * from student where 考场='" + Form6.str4 + "'";
                string str3_5 = "select * from student where 座位号='" + Form6.str5 + "'";
                if (Form6.str1 != "")
                {
                    if (str3 == "")
                        str3 = str3_1;
                    else
                        str3 += "AND 学号='" + Form6.str1 + "'";
                }
                if (Form6.str2 != "")
                {
                    if (str3 == "")
                        str3 = str3_2;
                    else
                        str3 += "AND 姓名='" + Form6.str2 + "'";
                }
                if (Form6.str3 != "")
                {
                    if (str3 == "")
                        str3 = str3_3;
                    else
                        str3 += "AND 性别='" + Form6.str3 + "'";
                }
                if (Form6.str4 != "")
                {
                    if (str3 == "")
                        str3 = str3_4;
                    else
                        str3 += "AND 考场='" + Form6.str4 + "'";
                }
                if (Form6.str5 != "")
                {
                    if (str3 == "")
                        str3 = str3_5;
                    else
                        str3 += "AND 座位号='" + Form6.str5 + "'";
                }
                if (str3 != "")
                    iBaby.DBOperation.BindDB(dataGridView1, str3, "student");
                else
                    MessageBox.Show("没有选择查询条件！");
            }
        }

        private void btn_mod_Click(object sender, EventArgs e)
        {
            /*
             * 修改功能是修改指定学号的考生的信息，可以修改单个信息，也可以修改多个信息。
             * 输入需要修改考场和座位号有格式限制，且学号不能为空！
             */
            MessageBox.Show("修改是修改指定学号的考生的信息，可以单个修改，也可以多个修改！");
            Form7 f4 = new Form7();
            f4.ShowDialog();
            if (Form7.flag == true)
            {
                string str4 = "";
                Regex reg1 = new Regex("^\\d{4}$");
                Regex reg2 = new Regex("^\\d{1,2}$");
                if (!reg1.IsMatch(Form7.str4))
                {
                    MessageBox.Show("考场格式错误（格式为：4个数字），请重新输入！");
                }
                if (!reg2.IsMatch(Form7.str5))
                {
                    MessageBox.Show("座位号格式错误（格式为：1-2个数字），请重新输入！");
                }
                else
                {
                    if (Form7.str1 != "")
                    {
                        if (Form7.str2 != "")
                            str4 = "update student set 姓名='" + Form7.str2 + "' where 学号='" + Form7.str1 + "'";
                        if (Form7.str3 != "")
                            str4 = "update student set 性别='" + Form7.str3 + "' where 学号='" + Form7.str1 + "'";
                        if (Form7.str4 != "")
                            str4 = "update student set 考场='" + Form7.str4 + "' where 学号='" + Form7.str1 + "'";
                        if (Form7.str5 != "")      //有可能出现bug，bug为当管理员修改座位号时，有可能当前考场的座位号已经存在，但其还是可以进行修改
                        {
                            str4 = "update student set 座位号='" + Form7.str5 + "' where 学号='" + Form7.str1 + "'";
                        }
                        if (str4 != "")
                        {
                            iBaby.DBOperation.exesql(str4);
                            iBaby.DBOperation.BindDB(dataGridView1, constr, "student");
                        }
                        else
                        {
                            MessageBox.Show("没有输入需要修改的信息！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("修改功能是根据学号来进行的，所以学号不能为空！");
                    }
                }
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();//退出程序
        }
    }
}
