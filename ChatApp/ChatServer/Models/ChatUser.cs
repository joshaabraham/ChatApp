using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class ChatUser
    {
        public string ConnectionId { get; set; }
        public string ChatUsername { get; set; }
        public DateTime ActionTime { get; set; }
    }
}
