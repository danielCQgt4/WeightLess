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
                        idActivityAssistance = -5
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
            } else if (user != null) {
                usu = new WsUserModel {
                    user = user,
                    connectionsId = new List<string>(),
                    idActivityAssistance = -5
                };
                usu.connectionsId.Add(Context.ConnectionId);
                UserConnections.Add(usu.user.idUser, usu);
                return usu;
            }
            return null;
        }

        private string[] GetExcepts(WsUserModel usu) {
            List<string> list = new List<string>();
            foreach (var userCon in UserConnections) {
                if (userCon.Key != usu.user.idUser) {
                    foreach (var con in userCon.Value.connectionsId) {
                        list.Add(con);
                    }
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
            WsUserModel usu = GetCurUser();
            if (usu != null) {
                IActivityDAL dalAct = new ActivityImpl();
                string[] t = dalAct.GetCurTime(usu.idActivityAssistance).Split(':');
                int hr = Convert.ToInt32(t[0]);
                int mm = Convert.ToInt32(t[1]);
                int ss = Convert.ToInt32(t[2]);
                string actualTime;
                object res;
                for (int h = hr; h < 3; h++) {
                    if (usu.idActivityAssistance == -5) {
                        break;
                    }
                    for (int m = mm; m < 60; m++) {
                        for (int s = ss; s < 60; s++) {
                            if (usu.idActivityAssistance != -5) {
                                actualTime = FixTime(h) + ":" + FixTime(m) + ":" + FixTime(s);
                                res = dalAct.UpdateTime(actualTime, usu.idActivityAssistance);
                                if (res != null) {
                                    Clients.AllExcept(GetExcepts(usu)).UpdateTime(res);
                                }
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
                if (usu.idActivityAssistance != -5) {
                    actualTime = "03:00:00";
                    res = dalAct.UpdateTime(actualTime, usu.idActivityAssistance);
                    if (res != null) {
                        Clients.AllExcept(GetExcepts(usu)).UpdateTime(res);
                    }
                    bool res2 = dalAct.StopTime(usu.idActivityAssistance, usu.user.idUser);
                    if (res2) {
                        usu.idActivityAssistance = -5;
                        Clients.AllExcept(GetExcepts(usu)).StopCurActivitie(res);
                    }
                }
                try {
                    Thread.CurrentThread.Abort();
                } catch (Exception e) {

                }
            }
        }

        [AuthorizeRole(Role.C)]
        public void ChangeTime(int id) {
            WsUserModel usu = GetCurUser();
            if (usu != null) {
                if (id != usu.idActivityAssistance && usu.idActivityAssistance != -5) {
                    IActivityDAL dalAct = new ActivityImpl();
                    bool res = dalAct.StopTime(id, usu.user.idUser);
                    if (res) {
                        usu.idActivityAssistance = -5;
                        Clients.AllExcept(GetExcepts(usu)).StopCurActivitie(res);
                    }
                } else {
                    usu.idActivityAssistance = id;
                    if (usu.thread == null || usu.idActivityAssistance == -5) {
                        if (usu.thread != null) {
                            try {
                                usu.thread.Abort();
                                usu.thread = null;
                            } catch (Exception e) {

                            }
                        }
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
            }
        }

        [AuthorizeRole(Role.C)]
        public void StopSessionActivity(int id) {
            WsUserModel usu = GetCurUser();
            if (usu != null) {
                IActivityDAL dalAct = new ActivityImpl();
                bool res = dalAct.StopTime(id, usu.user.idUser);
                if (res) {
                    if (usu.thread != null) {
                        try {
                            usu.thread.Abort();
                            usu.thread = null;
                            Clients.AllExcept(GetExcepts(usu)).UpdateTime(new { stop = true });
                        } catch (Exception e) {

                        }
                    }
                    usu.idActivityAssistance = -5;
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