using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;

namespace TF2_ServerManager
{
    class MainApp
    {
        private MainForm m_Form = null;
        System.Windows.Forms.Timer m_checkTimer;

        public bool Initialize(MainForm form)
        {
            m_Form = form;

            m_checkTimer = new System.Windows.Forms.Timer();

            m_checkTimer.Interval = 4000;
            m_checkTimer.Tick += M_checkTimer_Tick;

            m_checkTimer.Enabled = true;

            EventRegist();

            // 데이터 불러오기
            string folder = Path.GetDirectoryName(Application.ExecutablePath);
            FileInfo fi = new FileInfo(folder + "\\" + "TF2_ServerManager.dat");
            if (fi.Exists)
            {
                FileStream fs = new FileStream(folder + "\\" + "TF2_ServerManager.dat", FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                m_checkTimer.Interval = br.ReadInt32();
                ServerData.m_MaxProcessTryCnt = br.ReadInt32();
                ServerData.m_MaxNetworkTryCnt = br.ReadInt32();

                int cnt = br.ReadInt32();
                for (int i = 0; i < cnt; ++i)
                {
                    ServerData data = new ServerData();

                    // IP, Port, Local여부, 실행파일경로, 환경변수
                    data.m_IPAddress = br.ReadString(); // IP
                    data.m_port = br.ReadInt32(); // port
                    data.m_ServerDirectory = br.ReadString(); // Directory
                    data.m_LaunchOption = br.ReadString(); // launchOption
                    data.m_useLocalIP = br.ReadBoolean(); // useLocalIP
                    if (false == data.m_useLocalIP)
                        data.m_PStat = ProcessStatus.Not_Local;

                    ListViewItem item = new ListViewItem();

                    data.InitServerData(m_Form, item, m_Form.listView_Player);
                    data.InitSocket();

                    item.Tag = data;

                    item.Text = "";
                    item.SubItems.Add(""); // IP
                    item.SubItems.Add(""); // Port
                    item.SubItems.Add(""); // Players
                    item.SubItems.Add(""); // Map
                    item.SubItems.Add(""); // PStatus
                    item.SubItems.Add(""); // SStatus

                    item.UseItemStyleForSubItems = false;

                    m_Form.ServerListView.Items.Add(item);

                    if (null != data.m_Thread && true == data.m_Thread.IsAlive)
                        continue;

                    data.m_Thread = new Thread(new ThreadStart(data.CheckServer));
                    data.m_Thread.Start();
                }

                br.Close();
                fs.Close();
            }
            //////////////////////////////////////


            return true;
        }

        private void M_checkTimer_Tick(object sender, EventArgs e)
        {
            foreach(ListViewItem item in m_Form.ServerListView.Items)
            {
                ServerData data = (ServerData)item.Tag;
                if (null != data.m_Thread && true == data.m_Thread.IsAlive)
                    continue;

                data.m_Thread = new Thread(new ThreadStart(data.CheckServer));
                data.m_Thread.Start();
            }
        }

        private void EventRegist()
        {
            m_Form.addServerToolStripMenuItem.Click += NewServer;
            m_Form.settingToolStripMenuItem.Click += Options;
            m_Form.ServerListView.MouseClick += ChangePlayerListView;
            m_Form.listView_Player.ColumnClick += ListView_Player_ColumnClick;
            SetServerListViewMenu();
        }

        private void Options(object sender, EventArgs e)
        {
            Setting options = new Setting();

            options.numericUpDown_Interval.Value = (int)(m_checkTimer.Interval / 1000);
            options.numericUpDown_Process.Value = ServerData.m_MaxProcessTryCnt;
            options.numericUpDown_Network.Value = ServerData.m_MaxNetworkTryCnt;

            if (options.ShowDialog() == DialogResult.OK)
            {
                m_checkTimer.Interval = (int)options.numericUpDown_Interval.Value * 1000;
                ServerData.m_MaxProcessTryCnt = (int)options.numericUpDown_Process.Value;
                ServerData.m_MaxNetworkTryCnt = (int)options.numericUpDown_Network.Value;
                SaveFile();
            }
            options.Dispose();
        }

        private void ChangePlayerListView(object sender, MouseEventArgs e)
        {
            if(true == m_Form.ServerListView.Focused)
            {
                ServerData data = (ServerData)m_Form.ServerListView.FocusedItem.Tag;
                if (null != data.m_Thread && true == data.m_Thread.IsAlive)
                    return;

                data.m_Thread = new Thread(new ThreadStart(data.CheckServer));
                data.m_Thread.Start();
            }
        }

        private void ListView_Player_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == (int)PlayerListItems.Name)
                return;

            if(m_Form.listView_Player.Sorting == SortOrder.Ascending || m_Form.listView_Player.Sorting == SortOrder.None)
            {
                m_Form.listView_Player.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                m_Form.listView_Player.Sorting = SortOrder.Descending;
            }
            else
            {
                m_Form.listView_Player.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                m_Form.listView_Player.Sorting = SortOrder.Ascending;
            }
        }

        private void SetServerListViewMenu()
        {
            m_Form.m_ListViewMenu = new ContextMenu();
            MenuItem mNewServer = new MenuItem();
            MenuItem mEditServer = new MenuItem();
            MenuItem mDeleteServer = new MenuItem();
            
            MenuItem mMonitorPause = new MenuItem();
            MenuItem mRestart = new MenuItem();

            mNewServer.Text = "Add Server";
            mEditServer.Text = "Edit Server";
            mDeleteServer.Text = "Delete Server";

            mMonitorPause.Text = "Pause Monitor";
            mRestart.Text = "Restart Server";

            mNewServer.Click += NewServer;
            mEditServer.Click += EditServer;
            mDeleteServer.Click += DeleteServer;

            mMonitorPause.Click += PauseMonitor;
            mRestart.Click += RestartServer;

            m_Form.m_ListViewMenu.MenuItems.Add(mNewServer);
            m_Form.m_ListViewMenu.MenuItems.Add(mEditServer);
            m_Form.m_ListViewMenu.MenuItems.Add(mDeleteServer);

            m_Form.m_ListViewMenu.MenuItems.Add("-");

            m_Form.m_ListViewMenu.MenuItems.Add(mMonitorPause);
            m_Form.m_ListViewMenu.MenuItems.Add(mRestart);

            m_Form.m_ListViewMenu.MenuItems[(int)ListViewMenu.Edit].Enabled = false; // Edit
            m_Form.m_ListViewMenu.MenuItems[(int)ListViewMenu.Delete].Enabled = false; // Delete
            m_Form.m_ListViewMenu.MenuItems[(int)ListViewMenu.Pause].Enabled = false; // MonitorPause
            m_Form.m_ListViewMenu.MenuItems[(int)ListViewMenu.Restart].Enabled = false; // Restart
        }

        private void RestartServer(object sender, EventArgs e)
        {
            if (false == m_Form.ServerListView.Focused)
                return;
            ((ServerData)m_Form.ServerListView.FocusedItem.Tag).RestartProcess();
        }

        private void PauseMonitor(object sender, EventArgs e)
        {
            if (false == m_Form.ServerListView.Focused)
                return;
            ((ServerData)m_Form.ServerListView.FocusedItem.Tag).PauseServer();
        }

        private void SaveFile()
        {
            string folder = Path.GetDirectoryName(Application.ExecutablePath);
            FileStream fs = new FileStream(folder + "\\" + "TF2_ServerManager.dat", FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(m_checkTimer.Interval);
            bw.Write(ServerData.m_MaxProcessTryCnt);
            bw.Write(ServerData.m_MaxNetworkTryCnt);

            bw.Write(m_Form.ServerListView.Items.Count);
            foreach (ListViewItem item in m_Form.ServerListView.Items)
            {
                ServerData data = (ServerData)item.Tag;
                // IP, Port, Local여부, 실행파일경로, 환경변수
                bw.Write(data.m_IPAddress);
                bw.Write(data.m_port);
                bw.Write(data.m_ServerDirectory);
                bw.Write(data.m_LaunchOption);
                bw.Write(data.m_useLocalIP);
            }

            bw.Close();
            fs.Close();
        }

        private void DeleteServer(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete the selected server?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int focusIdx = m_Form.ServerListView.FocusedItem.Index;
                m_Form.ServerListView.Items.RemoveAt(focusIdx);
                SaveFile();
            }
        }

        private void NewServer(object sender, EventArgs e)
        {
            NewServer newServer = new NewServer();
            if (newServer.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem();

                newServer.Data.InitServerData(m_Form, item, m_Form.listView_Player);
                newServer.Data.InitSocket();

                item.Tag = newServer.Data;

                item.Text = "";
                item.SubItems.Add(""); // IP
                item.SubItems.Add(""); // Port
                item.SubItems.Add(""); // Players
                item.SubItems.Add(""); // Map
                item.SubItems.Add(""); // PStatus
                item.SubItems.Add(""); // SStatus

                item.UseItemStyleForSubItems = false;

                m_Form.ServerListView.Items.Add(item);

                if (null != newServer.Data.m_Thread && true == newServer.Data.m_Thread.IsAlive)
                    return;

                newServer.Data.m_Thread = new Thread(new ThreadStart(newServer.Data.CheckServer));
                newServer.Data.m_Thread.Start();

                SaveFile();
            }

            newServer.Dispose();
        }

        private void EditServer(object sender, EventArgs e)
        {
            NewServer editServer = new NewServer();
            editServer.Text = "Edit Server";

            ServerData data = (ServerData)m_Form.ServerListView.FocusedItem.Tag;

            editServer.textBox_IPAddress.Text = data.m_IPAddress;
            editServer.textBox_Port.Text = data.m_port.ToString();
            editServer.textBox_FilePath.Text = data.m_ServerDirectory;
            editServer.textBox_Launch.Text = data.m_LaunchOption;
            editServer.checkBox_useLocalP.Checked = data.m_useLocalIP;

            if(true == data.m_useLocalIP)
            {
                editServer.textBox_IPAddress.Enabled = false;
                editServer.textBox_FilePath.Enabled = true;
                editServer.textBox_Launch.Enabled = true;
            } 
            else
            {
                editServer.textBox_IPAddress.Enabled = true;
                editServer.textBox_FilePath.Enabled = false;
                editServer.textBox_Launch.Enabled = false;
            }

            if (editServer.ShowDialog() == DialogResult.OK)
            {
                data.m_IPAddress = editServer.Data.m_IPAddress;
                data.m_port = editServer.Data.m_port;
                data.m_ServerDirectory = editServer.Data.m_ServerDirectory;
                data.m_LaunchOption = editServer.Data.m_LaunchOption;

                data.InitSocket();
                
                if (null != data.m_Thread && true == data.m_Thread.IsAlive)
                    return;

                data.m_Thread = new Thread(new ThreadStart(data.CheckServer));
                data.m_Thread.Start();

                SaveFile();
            }

            editServer.Dispose();
        }

        public static string BufferToString(byte[] buffer, ref int idx)
        {
            int endIdx = idx; // last word is -1
            for (; endIdx < buffer.Length; ++endIdx)
            {
                if (buffer[endIdx] == 0)
                    break;
            }

            int strLength = endIdx - idx;

            if (0 >= strLength || strLength >= 256)
                return null;

            byte[] strBuffer = new byte[strLength];
            Array.Copy(buffer, idx, strBuffer, 0, strLength); // idx ~ endIdx - 1

            idx = endIdx + 1;

            return Encoding.UTF8.GetString(strBuffer);
        }

        public void Update()
        {
            Thread.Sleep(20);
        }
    }
}
