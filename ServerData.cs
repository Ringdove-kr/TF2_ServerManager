using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace TF2_ServerManager
{
    public class ServerData
    {
        [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);

        public Thread m_Thread;

        private MainForm m_Form;
        public ListViewItem m_ListViewItem;
        public DoubleBufferListView m_ListViewPlayers;
        
        public string m_IPAddress;
        public int m_port;
        public ProcessStatus m_PStat;
        public NetworkStatus m_NStat;

        public string m_ServerDirectory;
        public string m_LaunchOption;
        public int m_ProcessTryCnt;
        public int m_NetworkTryCnt;

        RQST_INFO m_rqstInfo = null;
        List<RQST_PLAYER> m_rqstPlayerList = null;

        public static int m_MaxProcessTryCnt = 2;
        public static int m_MaxNetworkTryCnt = 10;

        public bool m_isActive;
        public bool m_useLocalIP;

        // UDP
        EndPoint m_remoteEP = null;
        Socket m_Socket = null;
        IPEndPoint m_serverEndPoint = null;

        public ServerData()
        {
            m_IPAddress = "";
            m_port = 0;
            m_ServerDirectory = "-";
            m_LaunchOption = "";
            m_PStat = ProcessStatus.Lost;
            m_NStat = NetworkStatus.Waiting;
            m_ProcessTryCnt = 0;
            m_NetworkTryCnt = 0;

            m_isActive = true;

            m_rqstInfo = new RQST_INFO();
            m_rqstPlayerList = new List<RQST_PLAYER>();
        }

        public void PauseServer()
        {
            if (true == m_isActive)
            {
                m_isActive = false;
                if (true == m_useLocalIP)
                    m_PStat = ProcessStatus.Pause;
                m_NStat = NetworkStatus.Pause;
            }
            else
            {
                m_isActive = true;
                if (true == m_useLocalIP)
                    m_PStat = ProcessStatus.Lost;
                m_NStat = NetworkStatus.Timeout;

                if (null != m_Thread && true == m_Thread.IsAlive)
                    return;
            }

            m_Thread = new Thread(new ThreadStart(CheckServer));
            m_Thread.Start();
        }

        public void CheckServer()
        {
            ProcessCheck();
            NetworkCheck();

            if(true == m_Form.Created)
                m_Form.Invoke(new MethodInvoker(RefreshItem));
        }

        public void InitServerData(MainForm form, ListViewItem listViewItem, DoubleBufferListView listViewPlayers)
        {
            m_Form = form;
            m_ListViewItem = listViewItem;
            m_ListViewPlayers = listViewPlayers;
        }

        public void InitSocket()
        {
            m_remoteEP = new IPEndPoint(IPAddress.Any, 0);

            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_Socket.ReceiveTimeout = 500;
            m_Socket.SendTimeout = 500;

            m_serverEndPoint = new IPEndPoint(IPAddress.Parse(m_IPAddress), m_port);
        }

        public void RefreshItem()
        {
            // Print Server Info
            m_ListViewItem.Text = m_rqstInfo.serverName;
            m_ListViewItem.SubItems[(int)ServerListItems.IP].Text = m_IPAddress; // IP
            m_ListViewItem.SubItems[(int)ServerListItems.Port].Text = m_port.ToString(); // Port

            if(0 < m_rqstInfo.maxPlayers && true == m_isActive && m_NStat == NetworkStatus.Online)
                m_ListViewItem.SubItems[(int)ServerListItems.Players].Text = (m_rqstInfo.players - m_rqstInfo.bots) + "/" + m_rqstInfo.maxPlayers + " (" + m_rqstInfo.bots + ")"; // Players
            else
                m_ListViewItem.SubItems[(int)ServerListItems.Players].Text = "-";

            if (true == m_isActive && m_NStat == NetworkStatus.Online)
                m_ListViewItem.SubItems[(int)ServerListItems.Map].Text = m_rqstInfo.map; // Map
            else
                m_ListViewItem.SubItems[(int)ServerListItems.Map].Text = "-";

            string PStatText;
            if (m_PStat == ProcessStatus.Running)
            {
                PStatText = "Running";
                m_ListViewItem.SubItems[(int)ServerListItems.PStat].ForeColor = System.Drawing.Color.Green;
            }
            else if (m_PStat == ProcessStatus.Lost)
            {
                PStatText = "Lost(" + m_ProcessTryCnt + "/" + m_MaxProcessTryCnt + ")";
                m_ListViewItem.SubItems[(int)ServerListItems.PStat].ForeColor = System.Drawing.Color.Red;
            }
            else if (m_PStat == ProcessStatus.Waiting)
            {
                PStatText = "Waiting";
                m_ListViewItem.SubItems[(int)ServerListItems.PStat].ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else if(m_PStat == ProcessStatus.Pause)
            {
                PStatText = "Pause Monitor";
                m_ListViewItem.SubItems[(int)ServerListItems.PStat].ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else
            {
                PStatText = "-";
                m_ListViewItem.SubItems[(int)ServerListItems.PStat].ForeColor = System.Drawing.SystemColors.WindowText;
            }
            m_ListViewItem.SubItems[(int)ServerListItems.PStat].Text = PStatText; // PStatus

            string NStatText;
            if (m_NStat == NetworkStatus.Online)
            {
                NStatText = "Online";
                m_ListViewItem.SubItems[(int)ServerListItems.NStat].ForeColor = System.Drawing.Color.Green;
            }
            else if (m_NStat == NetworkStatus.Timeout)
            {
                NStatText = "Timeout(" + m_NetworkTryCnt + "/" + m_MaxNetworkTryCnt + ")";
                m_ListViewItem.SubItems[(int)ServerListItems.NStat].ForeColor = System.Drawing.Color.Red;
            }
            else if(m_NStat == NetworkStatus.Offline)
            {
                NStatText = "Offline";
                m_ListViewItem.SubItems[(int)ServerListItems.NStat].ForeColor = System.Drawing.Color.Red;
            }
            else if(m_NStat == NetworkStatus.Pause)
            {
                NStatText = "Pause Monitor";
                m_ListViewItem.SubItems[(int)ServerListItems.NStat].ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else
            {
                NStatText = "-";
                m_ListViewItem.SubItems[(int)ServerListItems.NStat].ForeColor = System.Drawing.SystemColors.WindowText;
            }
            m_ListViewItem.SubItems[(int)ServerListItems.NStat].Text = NStatText;

            // Print Player Info
            if (false == m_ListViewItem.Focused)
                return;

            m_Form.label_ServerPlayer.Text = "Online Players" + " (" + m_rqstInfo.serverName + ")";
            m_ListViewPlayers.Items.Clear();

            if (false == m_isActive)
                return;

            foreach(RQST_PLAYER player in m_rqstPlayerList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = player.playerName;
                item.SubItems.Add(player.score.ToString());

                int totalTime = (int)player.time;
                int hour = totalTime / 3600;
                totalTime -= hour * 3600;

                int min = (totalTime % 3600) / 60;
                int sec = totalTime - (min * 60);

                item.SubItems.Add(hour + ":" + min.ToString("00") + ":" + sec.ToString("00"));

                m_ListViewPlayers.Items.Add(item);
            }
                
        }

        public void RestartProcess()
        {
            if (true == m_useLocalIP)
                m_PStat = ProcessStatus.Waiting;

            m_NStat = NetworkStatus.Waiting;

            m_NetworkTryCnt = 0;
            m_ProcessTryCnt = 0;

            m_Socket.Close();
            InitSocket();

            KillProcess();
            ProcessExecution();
        }

        public void KillProcess()
        {
            Process[] processList = Process.GetProcessesByName("srcds");
            bool isRunning = false;

            for (int j = 0; j < processList.Length; ++j)
            {
                string processDir;

                try { processDir = processList[j].MainModule.FileName; }
                catch { continue; };

                if (m_ServerDirectory == processDir)
                {
                    processList[j].Kill();
                    break;
                }
            }

            if(false == isRunning)
                m_PStat = ProcessStatus.Lost;
        }

        public void ProcessExecution()
        {
            string arguments = m_LaunchOption + " -port " + m_port.ToString();

            ProcessStartInfo serverProcessStartInfo = new ProcessStartInfo(m_ServerDirectory);
            serverProcessStartInfo.Arguments = arguments;
            Process p = Process.Start(serverProcessStartInfo);
        }

        public void ProcessCheck()
        {
            if (false == m_isActive)
                return;

            if (m_PStat == ProcessStatus.Not_Local)
                return;

            bool isRunning = false;

            Process[] processList = Process.GetProcessesByName("srcds");

            for (int j = 0; j < processList.Length; ++j)
            {
                string processDir;

                try { processDir = processList[j].MainModule.FileName; }
                catch { continue; };

                if (m_ServerDirectory == processDir)
                {
                    isRunning = true;
                    SetWindowText(processList[j].MainWindowHandle, m_rqstInfo.serverName);
                    break;
                }
            }

            if (isRunning)
            {
                m_ProcessTryCnt = 0;
                m_PStat = ProcessStatus.Running;
            }
            else
            {
                if (m_ProcessTryCnt >= m_MaxProcessTryCnt)
                {
                    m_ProcessTryCnt = 0;
                    m_NetworkTryCnt = 0;
                    m_PStat = ProcessStatus.Waiting;
                    ProcessExecution();
                }
                else
                {
                    ++m_ProcessTryCnt;
                    m_NetworkTryCnt = 0;
                    m_PStat = ProcessStatus.Lost;
                    m_NStat = NetworkStatus.Timeout;
                }
            }
        }

        public void NetworkCheck()
        {
            if (false == m_isActive)
                return;

            if (m_PStat == ProcessStatus.Lost)
                return;

            bool isOnline = RecvInfo();

            if (false == isOnline)
            {
                m_Socket.Close();
                InitSocket();

                if (m_NetworkTryCnt >= m_MaxNetworkTryCnt)
                {
                    m_NetworkTryCnt = 0;
                    if(true == m_useLocalIP)
                    {
                        RestartProcess();
                    }
                }
                else
                {
                    if(m_useLocalIP == true)
                    {
                        m_NStat = NetworkStatus.Timeout;
                        ++m_NetworkTryCnt;
                    }
                    else
                    {
                        m_NStat = NetworkStatus.Offline;
                    }
                }
                return;
            }

            m_NetworkTryCnt = 0;
            RecvPlayer();
        }

        private void RecvPlayer()
        {
            int recv = 0;
            byte[] buffer = new byte[1400];

            try
            {
                recv = m_Socket.SendTo(QueryMsg.PlayerQuery, m_serverEndPoint);
                recv = m_Socket.ReceiveFrom(buffer, ref m_remoteEP);
            }
            catch (Exception)
            {
                return;
            }

            if ((byte)MsgHeader.A2S_CHALLENGE != buffer[4])
            {
                m_Socket.Close();
                InitSocket();
                return;
            }

            byte[] challengeQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x55, buffer[5], buffer[6], buffer[7], buffer[8] };
            buffer = new byte[1400];
            try
            {
                recv = m_Socket.SendTo(challengeQuery, m_serverEndPoint);
                recv = m_Socket.ReceiveFrom(buffer, ref m_remoteEP);
            }
            catch (Exception)
            {
                return;
            }

            if ((byte)MsgHeader.A2S_PLAYER != buffer[4])
            {
                m_Socket.Close();
                InitSocket();
                return;
            }

            foreach (RQST_PLAYER exstPlayer in m_rqstPlayerList)
                exstPlayer.isCheck = false;

            int idx = 5;

            int numPlayer = buffer[idx]; // player cnt
            ++idx;

            for (int i = 0; i < numPlayer; ++i)
            {
                RQST_PLAYER player = new RQST_PLAYER();
                ++idx;

                player.playerName = MainApp.BufferToString(buffer, ref idx);
                if (String.IsNullOrEmpty(player.playerName))
                    continue;

                byte[] scoreBuffer = new byte[4];
                Array.Copy(buffer, idx, scoreBuffer, 0, 4);
                player.score = (int)(scoreBuffer[0] + (scoreBuffer[1] << 8) + (scoreBuffer[2] << 16) + (scoreBuffer[3] << 24));
                idx += 4;

                byte[] timeBuffer = new byte[4];
                Array.Copy(buffer, idx, timeBuffer, 0, 4);
                player.time = BitConverter.ToSingle(timeBuffer, 0);
                idx += 4;

                int exstPlayerIdx = -1;
                for(int j = 0; j < m_rqstPlayerList.Count; ++j)
                {
                    if(m_rqstPlayerList[j].playerName == player.playerName)
                    {
                        exstPlayerIdx = j;
                        break;
                    }
                }

                if (-1 == exstPlayerIdx)
                {
                    player.isCheck = true;
                    m_rqstPlayerList.Add(player);
                }
                else
                {
                    m_rqstPlayerList[exstPlayerIdx].playerName = player.playerName;
                    m_rqstPlayerList[exstPlayerIdx].score = player.score;
                    m_rqstPlayerList[exstPlayerIdx].time = player.time;
                    m_rqstPlayerList[exstPlayerIdx].isCheck = true;
                }
            }

            for (int i = 0; i < m_rqstPlayerList.Count;)
            {
                if (m_rqstPlayerList[i].isCheck == false)
                    m_rqstPlayerList.RemoveAt(i);
                else
                    ++i;
            }
        }

        private bool RecvInfo()
        {
            int recv = 0;
            byte[] buffer = new byte[1400];

            try
            {
                recv = m_Socket.SendTo(QueryMsg.InfoQuery, m_serverEndPoint);
                recv = m_Socket.ReceiveFrom(buffer, ref m_remoteEP);
            }
            catch (Exception)
            {
                return false;
            }

            //string requestBuffer = Encoding.UTF8.GetString(buffer).Substring(7);

            if((byte)MsgHeader.A2S_INFO != buffer[4])
            {
                return false;
            }


            m_rqstInfo.protocolVersion = buffer[5]; // Protocol Version

            int idx = 6;
            m_rqstInfo.serverName = MainApp.BufferToString(buffer, ref idx);
            if (String.IsNullOrEmpty(m_rqstInfo.serverName))
                return false;

            m_rqstInfo.map = MainApp.BufferToString(buffer, ref idx);
            if (String.IsNullOrEmpty(m_rqstInfo.map))
                return false;

            m_rqstInfo.gameDirectory = MainApp.BufferToString(buffer, ref idx);
            if (String.IsNullOrEmpty(m_rqstInfo.gameDirectory))
                return false;

            m_rqstInfo.gameName = MainApp.BufferToString(buffer, ref idx);
            if (String.IsNullOrEmpty(m_rqstInfo.gameName))
                return false;

            byte[] idBuffer = new byte[2];
            Array.Copy(buffer, idx, idBuffer, 0, 2); // idx ~ endIdx - 1
            m_rqstInfo.appID = (short)(idBuffer[0] + (idBuffer[1] << 8));
            idx += 2;

            m_rqstInfo.players = buffer[idx];
            m_rqstInfo.maxPlayers = buffer[idx + 1];
            m_rqstInfo.bots = buffer[idx + 2];
            m_rqstInfo.serverType = buffer[idx + 3];
            m_rqstInfo.os = buffer[idx + 4];
            m_rqstInfo.visibility = buffer[idx + 5];
            m_rqstInfo.vac = buffer[idx + 6];

            idx += 7;

            m_rqstInfo.gameVersion = MainApp.BufferToString(buffer, ref idx);
            if (String.IsNullOrEmpty(m_rqstInfo.gameVersion))
                return false;

            m_NStat = NetworkStatus.Online;
            return true;
        }
    }
}
