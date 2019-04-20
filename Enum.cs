using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF2_ServerManager
{
    public enum ProcessStatus { Lost, Running, Waiting, Not_Local, Pause }
    public enum NetworkStatus { Timeout, Online, Waiting, Offline, Pause }

    public enum ServerListItems { Name, IP, Port, Players, Map, PStat, NStat }
    public enum PlayerListItems { Name, Score, Time }

    public enum ListViewMenu { New, Edit, Delete, DivLine, Pause, Restart }

    public enum MsgHeader : byte
    {
        A2S_INFO = 0x49,
        A2S_PLAYER = 0x44,
        A2S_CHALLENGE = 0x41
    }

    public enum PacketId
    {
        Empty = 10,
        ExecCmd = 11
    }

    public enum PacketType
    {
        Auth = 3,
        AuthResponse = 2,
        Exec = 2,
        ExecResponse = 0
    }
}
