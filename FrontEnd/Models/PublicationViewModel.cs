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

        [Required]
        public DateTime datetime { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string type { get; set; }

        //TEMP
        public int likes { get; set; }
        public int disLikes { get; set; }

        public IEnumerable<Activity> publicationActivities { get; set; }

        public int idUser { get; set; }
        public User User { get; set; }


        public static PublicationViewModel Converter(Publication publication) {
            return new PublicationViewModel() {
                idPublication = publication.idPublication,
                datetime = publication.datetime,
                title = publication.title,
                description = publication.description,
                type = publication.type,
                likes = publication.likes,
                disLikes = publication.disLikes,
                idUser = publication.idUser
            };
        }

        public static PublicationViewModel Converter(PublicationViewModel publication) {
            return new PublicationViewModel() {
                idPublication = publication.idPublication,
                datetime = publication.datetime,
                title = publication.title,
                description = publication.description,
                type = publication.type,
                likes = publication.likes,
                disLikes = publication.disLikes,
                idUser = publication.idUser
            };
        }

    }
}