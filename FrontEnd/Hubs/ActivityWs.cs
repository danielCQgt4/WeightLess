using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Backend.DAL;
using Backend.Entity;
using FrontEnd.Models;
using Microsoft.AspNet.SignalR;

namespace FrontEnd.Hubs {
    public class ActivityWs : Hub {

        private static Dictionary<int, WsUserModel> UserConnections = new Dictionary<int, WsUserModel>();

        public void Hello() {
            Clients.All.hello();
        }

        public override Task OnConnected() {
            if (Context.User.Identity.IsAuthenticated) {
                IUserDAL us = new UserDALImp();
                User user = us.Get_User(Convert.ToInt32(Context.Request.User.Identity.Name));
                WsUserModel usu;
                if (UserConnections.TryGetValue(user.idUser, out usu)) {
                    usu.connectionsId.Add(Context.ConnectionId);
                } else {
                    usu = new WsUserModel();
                    usu.user = user;
                    usu.connectionsId = new List<string>();
                    usu.connectionsId.Add(Context.ConnectionId);
                }
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            if (Context.User.Identity.IsAuthenticated) {
                IUserDAL us = new UserDALImp();
                User user = us.Get_User(Convert.ToInt32(Context.Request.User.Identity.Name));
                WsUserModel usu;
                if (UserConnections.TryGetValue(user.idUser, out usu)) {
                    usu.connectionsId = usu.connectionsId.FindAll(o => !o.Equals(Context.ConnectionId));
                    if (usu.connectionsId.Count == 0) {
                        UserConnections.Remove(usu.user.idUser);
                    }
                }
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}