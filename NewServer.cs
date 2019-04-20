using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TF2_ServerManager
{
    public partial class NewServer : Form
    {
        private ServerData data;

        internal ServerData Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public NewServer()
        {
            data = new ServerData();
            InitializeComponent();

            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    localIP = ip.ToString();
            }

            textBox_IPAddress.Text = localIP;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.InitialDirectory = "c:\\";
            openfile.Filter = "Source Dedicated Server|srcds.exe";

            if(openfile.ShowDialog() == DialogResult.OK)
            {
                textBox_FilePath.Text = openfile.FileName;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (button_OK.DialogResult == DialogResult.OK)
                return;

            data.m_IPAddress = textBox_IPAddress.Text;
            data.m_port = int.Parse(textBox_Port.Text);

            if(true == checkBox_useLocalP.Checked)
            {
                try
                {
                    FileInfo fi = new FileInfo(textBox_FilePath.Text);
                    if (false == fi.Exists)
                    {
                        MessageBox.Show("The file does not exist.");
                        return;
                    }
                }

                catch(Exception)
                {
                    MessageBox.Show("The file does not exist.");
                    return;
                }

                if(false == textBox_Launch.Text.Contains("-console"))
                {
                    MessageBox.Show("Launch options must contain \"-console\"");
                    return;
                }

                if (false == textBox_Launch.Text.Contains("-game tf"))
                {
                    MessageBox.Show("Launch options must contain \"-game tf\"");
                    return;
                }

                data.m_ServerDirectory = textBox_FilePath.Text;
                data.m_LaunchOption = textBox_Launch.Text;
                data.m_useLocalIP = true;
            }
            else
            {
                data.m_PStat = ProcessStatus.Not_Local;
                data.m_ServerDirectory = "";
                data.m_LaunchOption = "";
                data.m_useLocalIP = false;
            }

            DialogResult = DialogResult.OK;
        }

        private void CheckBox_useLocalP_CheckedChanged(object sender, EventArgs e)
        {
            if (true == checkBox_useLocalP.Checked)
            {
                textBox_IPAddress.Enabled = false;
                textBox_FilePath.Enabled = true;
                textBox_Launch.Enabled = true;

                string localIP = "";
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        localIP = ip.ToString();
                }

                textBox_IPAddress.Text = localIP;
            }
            else
            {
                textBox_IPAddress.Enabled = true;
                textBox_FilePath.Enabled = false;
                textBox_Launch.Enabled = false;
            }
        }
    }
}
