using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace WindowsFormskaoshengsystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string path = @"数据库连接信息.txt";
            string[] content = File.ReadAllLines(path, Encoding.Default);
            if (content.Length==0)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new Form2());
            }
        }
    }
}
