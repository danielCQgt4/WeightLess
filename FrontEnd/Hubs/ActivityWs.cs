using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Backend.DAL;
using Backend.Entity;
using Backend.IMPL;
using FrontEnd.Models;
using Microsoft.AspNet.SignalR;

namespace FrontEnd.Hubs {
    public class ActivityWs : Hub {

        private static readonly Dictionary<int, WsUserModel> UserConnections = new Dictionary<int, WsUserModel>();

        public override Task OnConnected() {
            if (Context.User.Identity.IsAuthenticated) {
                IUserDAL us = new UserDALImp();
                User user = us.Get_User(Convert.ToInt32(Context.Request.User.Identity.Name));
                if (UserConnections.TryGetValue(user.idUser, out WsUserModel usu)) {
                    usu.connectionsId.Add(Context.ConnectionId);
                } else {
                    usu = new WsUserModel {
                        user = user,
                        connectionsId = new List<string>(),
                        actualTime = "00:00:00"
                    };
                    usu.connectionsId.Add(Context.ConnectionId);
                    UserConnections.Add(usu.user.idUser, usu);
                }
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            if (Context.User.Identity.IsAuthenticated) {
                IUserDAL us = new UserDALImp();
                User user = us.Get_User(Convert.ToInt32(Context.Request.User.Identity.Name));
                if (UserConnections.TryGetValue(user.idUser, out WsUserModel usu)) {
                    usu.connectionsId = usu.connectionsId.FindAll(o => !o.Equals(Context.ConnectionId));
                    if (usu.connectionsId.Count == 0) {
                        UserConnections.Remove(usu.user.idUser);
                    }
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        private WsUserModel GetCurUser() {
            IUserDAL us = new UserDALImp();
            User user = us.Get_User(Convert.ToInt32(Context.Request.User.Identity.Name));
            if (UserConnections.TryGetValue(user.idUser, out WsUserModel usu)) {
                return usu;
            }
            return null;
        }

        private string[] GetExcepts(WsUserModel usu) {
            List<string> list = new List<string>();
            foreach (var item in UserConnections) {
                if (item.Key != usu.user.idUser) {
                    list = list.Concat<string>(item.Value.connectionsId).ToList();
                }
            }
            return list.ToArray();
        }

        private string FixTime(int s) {
            if (s <= 9) {
                return "0" + s;
            }
            return s + "";
        }

        private void CalcTime() {
            int hr = 0, mm = 0, ss = 0;
            WsUserModel usu = GetCurUser();
            IActivityDAL dalAct = new ActivityImpl();
            usu.actualTime = FixTime(hr) + ":" + FixTime(mm) + ":" + FixTime(ss);
            for (int h = 0; h < 3; h++) {
                for (int m = 0; m < 60; m++) {
                    for (int s = 0; s < 60; s++) {
                        string[] t = dalAct.GetCurTime(usu.idActivityAssistance).Split(':');
                        hr = Convert.ToInt32(t[0]);
                        mm = Convert.ToInt32(t[1]);
                        ss = Convert.ToInt32(t[2]);
                        ss++;
                        if (ss >= 60) {
                            ss = 0;
                            mm++;
                            if (mm == 60) {
                                mm = 0;
                                hr++;
                            }
                        }
                        usu.actualTime = FixTime(hr) + ":" + FixTime(mm) + ":" + FixTime(ss);
                        object res = dalAct.UpdateTime(usu.actualTime, usu.idActivityAssistance);
                        if (res != null) {
                            Clients.AllExcept(GetExcepts(usu)).UpdateTime(res);
                        }
                        if (hr >= 3) {
                            usu.actualTime = FixTime(hr) + ":" + FixTime(mm) + ":" + FixTime(ss);
                            res = dalAct.UpdateTime(usu.actualTime, usu.idActivityAssistance);
                            if (res != null) {
                                Clients.AllExcept(GetExcepts(usu)).UpdateTime(res);
                            }
                            bool res2 = dalAct.StopTime(usu.idActivityAssistance, usu.user.idUser);
                            if (res2) {
                                Clients.AllExcept(GetExcepts(usu)).StopCurActivitie(res);
                            }
                            try {
                                Thread.CurrentThread.Abort();
                            } catch (Exception e) {

                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        [AuthorizeRole(Role.C)]
        public void ChangeTime(int id) {
            WsUserModel usu = GetCurUser();
            usu.idActivityAssistance = id;
            if (usu.thread == null) {
                usu.thread = new Thread(CalcTime);
                usu.thread.Start();
            } else {
                try {
                    usu.thread.Abort();
                    usu.thread = null;
                    Clients.AllExcept(GetExcepts(usu)).UpdateTime(new { stop = true });
                } catch (Exception e) {

                }
            }
        }

        [AuthorizeRole(Role.C)]
        public void StopSessionActivity(int id) {
            WsUserModel usu = GetCurUser();
            if (usu != null) {
                IActivityDAL dalAct = new ActivityImpl();
                bool res = dalAct.StopTime(id, usu.user.idUser);
                if (res) {
                    Clients.AllExcept(GetExcepts(usu)).StopCurActivitie(res);
                }
            }
        }

        [AuthorizeRole(Role.C)]
        public void StartActivity(string curUrl) {
            WsUserModel usu = GetCurUser();
            Clients.AllExcept(GetExcepts(usu)).NewActivity(curUrl);
        }

        //[AuthorizeRole(Role.C)]
        //public void StopCurTime(int id) {
        //    WsUserModel usu = GetCurUser();
        //    usu.thread.Abort();
        //    IActivityDAL dalAct = new ActivityImpl();
        //    string cur = dalAct.GetCurTime(id);
        //    Clients.AllExcept(GetExcepts(usu)).StopCurTime(cur);
        //}

    }
}