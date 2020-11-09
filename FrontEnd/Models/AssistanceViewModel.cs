using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {
    public class AssistanceViewModel {


        public int idAssistance { get; set; }
        public DateTime datetime { get; set; }
        public int idUser { get; set; }

        public static AssistanceViewModel Converter(Assistance assistance) {
            return new AssistanceViewModel() {
                idUser = assistance.idUser,
                datetime = assistance.datetime,
                idAssistance = assistance.idAssistance
            };
        }

        public static List<AssistanceViewModel> Converter(List<Assistance> assistance) {
            List<AssistanceViewModel> userViewModels = new List<AssistanceViewModel>();
            foreach (var u in assistance) {
                userViewModels.Add(Converter(u));
            }
            return userViewModels;
        }

        public static Assistance Converter(AssistanceViewModel assistance) {
            return new Assistance() {
                idUser = assistance.idUser,
                datetime = assistance.datetime,
                idAssistance = assistance.idAssistance
            };
        }
    }
}