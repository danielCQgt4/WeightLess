using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FrontEnd.Models {
    public class WsUserModel {

        public User user { get; set; }
        public string actualTime { get; set; }
        public int idActivityAssistance { get; set; }
        public Thread thread { get; set; }
        public List<string> connectionsId { get; set; }

    }
}