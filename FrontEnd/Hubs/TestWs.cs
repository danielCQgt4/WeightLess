using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace FrontEnd.Hubs {
    public class TestWs : Hub {

        //public void Hello() {
        //    Clients.All.hello();
        //}

        public void Send(string msj) {
            Clients.All.Listen(msj);
        }

        public override Task OnConnected() {
            return base.OnConnected();
        }
    }
}