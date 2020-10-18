using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {
    public class AssistanceViewModel {

        public int idAssistance { get; set; }
        public System.DateTime date { get; set; }
        public System.DateTime time { get; set; }
        public int idUser { get; set; }
    }
}