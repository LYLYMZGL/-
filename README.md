# Candidate-Management-System
该项目是使用C#语言实现的，基于对话框的考生管理系统！

# 1、功能介绍：
用户打开系统后，先进行数据库用户登录(只有第一次打开系统时需要数据库登录，第二次打开就不需要数据库登录)，然后进行用户登录界面，在登录窗口中输入账号和密码，通过数据库的链接校对，先判断账号和密码是否存在，若存在是否正确。若输入的是管理员账号与密码则进入管理员操作界面；若输入的是普通用户账号与密码则进入普通用户操作界面，并根据用户的自主选择给用户提供相应的服务。若账号与密码不正确，弹出对话框提示用户账号或密码错误后，点击确认重新输入。

# 2、程序流程图
![](https://user-gold-cdn.xitu.io/2019/9/30/16d81582a8c0c0d2?w=881&h=771&f=png&s=33022) 

# 3、系统功能实现
1、数据库操作类（DBOperation.cs）
```c#
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

```
2、数据库用户登录
```c#
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
            comboBox1.Items.Add("服务器名称");  
            comboBox2.Items.Add("角色");
            comboBox3.Items.Add("登录名");
            comboBox4.Items.Add("密码");
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d815edd302d7ac?w=348&h=339&f=png&s=7034)

3、用户登录界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d816048d10a57a?w=333&h=314&f=png&s=6512)

4、考生管理系统（管理员）主界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d8161c386d73c2?w=695&h=329&f=png&s=7640)

5、添加考生信息界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d8163aaced395a?w=305&h=338&f=png&s=7664)

6、删除考生信息界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d8165e5814c659?w=309&h=314&f=png&s=7609)

7、查询考生信息界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d81671f14dc1a9?w=314&h=333&f=png&s=7791)

8、修改考生信息界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d8168b1689d39c?w=310&h=329&f=png&s=7861)

9、考生管理系统(普通用户)主界面
```c#
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

```

![](https://user-gold-cdn.xitu.io/2019/9/30/16d8169ce28b82b1?w=810&h=339&f=png&s=9430)


