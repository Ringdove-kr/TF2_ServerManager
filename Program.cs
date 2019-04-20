using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace TF2_ServerManager
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process[] processList = Process.GetProcessesByName("TF2_ServerManager");
            if (processList.Length > 1)
                return;

            MainApp App = new MainApp();
            using (MainForm form = new MainForm())
            {
                if (App.Initialize(form))
                {
                    form.Show();
                    while (form.Created)
                    {
                        App.Update();
                        Application.DoEvents();
                    }
                }
                else
                    MessageBox.Show("MainApp Initialize Failed");
            }
            
        }
    }
}
