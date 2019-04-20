using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.NetworkInformation;
using System.Net;

namespace TF2_ServerManager
{
    public partial class MainForm : Form
    {
        private bool isMore = false;
        public ContextMenu m_ListViewMenu = null;

        public MainForm()
        {
            InitializeComponent();

            ServerListView.View = View.Details;
            listView_Player.View = View.Details;

            InitNotifyMenu();

            ServerListView.MouseUp += ServerListView_MouseUp;

            Size = new Size(838, 216);
        }

        private void ServerListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                RefreshListViewMenu();
                m_ListViewMenu.Show(ServerListView, e.Location);
            }
        }

        private void InitNotifyMenu()
        {
            ContextMenu notifyMenu = new ContextMenu();

            MenuItem mShowWindow = new MenuItem("Show Window");
            MenuItem mExit = new MenuItem("Exit");

            mShowWindow.Click += (object sender, EventArgs e) => { Show(); };
            mExit.Click += (object sender, EventArgs e) => { Show(); Application.Exit(); };

            notifyMenu.MenuItems.Add(mShowWindow);
            notifyMenu.MenuItems.Add("-");
            notifyMenu.MenuItems.Add(mExit);

            notifyIcon.ContextMenu = notifyMenu;
        }

        private void RefreshListViewMenu()
        {
            if (ServerListView.SelectedIndices.Count <= 0)
            {
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Edit].Enabled = false; // Edit
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Delete].Enabled = false; // Delete
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Pause].Enabled = false; // MonitorPause
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Restart].Enabled = false; // Restart

                m_ListViewMenu.MenuItems[(int)ListViewMenu.Pause].Text = "Pause Monitor";
            }
            else
            {
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Edit].Enabled = true; // Edit
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Delete].Enabled = true; // Delete
                m_ListViewMenu.MenuItems[(int)ListViewMenu.Pause].Enabled = true; // MonitorPause

                if(true == ((ServerData)ServerListView.FocusedItem.Tag).m_isActive)
                    m_ListViewMenu.MenuItems[(int)ListViewMenu.Pause].Text = "Pause Monitor";
                else
                    m_ListViewMenu.MenuItems[(int)ListViewMenu.Pause].Text = "Unpause Monitor";

                if (((ServerData)ServerListView.FocusedItem.Tag).m_useLocalIP)
                    m_ListViewMenu.MenuItems[(int)ListViewMenu.Restart].Enabled = true; // Restart
                else
                    m_ListViewMenu.MenuItems[(int)ListViewMenu.Restart].Enabled = false; // Restart
            }
        }

        private void button_more_Click(object sender, EventArgs e)
        {
            if(false == isMore)
            {
                Size = new Size(838, 560);
                isMore = true;
                button_more.Text = "▲";
            }
            else
            {
                Size = new Size(838, 216);
                isMore = false;
                button_more.Text = "▼";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();

            about.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                notifyIcon.ShowBalloonTip(2000);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }
    }
}
