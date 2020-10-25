﻿using Backend.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {
    public class PublicationViewModel {

        [Key]
        public int idPublication { get; set; }

        [Required]
        [DisplayName("Fecha")]
        public DateTime datetime { get; set; }

        [Required]
        [DisplayName("Título")]
        public string title { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string description { get; set; }

        [Required]
        [DisplayName("Tipo")]
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

        public static Publication Converter(PublicationViewModel publication) {
            return new Publication() {
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

        public static List<PublicationViewModel> Converter(List<Publication> publications) {
            List<PublicationViewModel> publicationsVM = new List<PublicationViewModel>();
            foreach (var p in publications) {
                publicationsVM.Add((PublicationViewModel)Converter(p));
            }
            return publicationsVM;
        }

    }
}