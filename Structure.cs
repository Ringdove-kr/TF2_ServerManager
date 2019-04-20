using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2_ServerManager
{
    public class RQST_INFO
    {
        public byte protocolVersion;
        public string serverName;
        public string map;
        public string gameDirectory;
        public string gameName;
        public short appID;
        public byte players;
        public byte maxPlayers;
        public byte bots;
        public byte serverType;
        public byte os;
        public byte visibility;
        public byte vac;
        public string gameVersion;
    }

    public class RQST_PLAYER
    {
        public string playerName;
        public int score;
        public float time;
        public bool isCheck;
    }
}
