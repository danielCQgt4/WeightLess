using Backend.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {
    public class PublicationViewModel {

        [Key]
        public int idPublication { get; set; }
        public System.DateTime date { get; set; }
        public System.DateTime time { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int idUser { get; set; }
        public int likes { get; set; }
        public int disLikes { get; set; }
        public string title { get; set; }

        public virtual IEnumerable<Publication_Activity> Publication_Activity { get; set; }
        public virtual User User { get; set; }

    }
}