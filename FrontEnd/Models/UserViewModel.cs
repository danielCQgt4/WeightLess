using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Backend.Entity;

namespace FrontEnd.Models {
    public class UserViewModel {
  

        [Key]
        public int idUser { get; set; }

        [Required]
        [MinLength(9)]
        [MaxLength(15)]
        [DisplayName("Cédula")]
        public string dni { get; set; }

        [Required]
        [StringLength(75)]
        [DisplayName("Nombre")]
        public string name { get; set; }

        [Required]
        [StringLength(75)]
        [DisplayName("Apellido")]
        public string lastName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Correo inválido.")]
        [Required]
        [StringLength(100)]
        [DisplayName("Correo")]
        public string email { get; set; }

        [Required]
        [StringLength(70)]
        [DisplayName("Contraseña")]
        public string password { get; set; }

        [Required]
        [StringLength(1)]
        [DisplayName("Tipo")]
        public string rol { get; set; }

        [Required]
        [DisplayName("Altura")]
        [Range(0, 210)]
        public decimal height { get; set; }

        [Required]
        [DisplayName("Peso")]
        [Range(0, 300)]
        [RegularExpression(@"^(\d\d\d\.)?\d\d$", ErrorMessage = "Peso inválido.")]
        public decimal weight { get; set; }

        [DisplayName("Estado")]
        public bool active { get; set; }

        public static UserViewModel Converter(User user) {
            return new UserViewModel() {
                idUser = user.idUser,
                dni = user.dni,
                name = user.name,
                lastName = user.lastName,
                email = user.email,
                password = user.password,
                rol = user.rol,
                height = user.height,
                weight = user.weight,
                active = user.active
            };
        }

        public static List<UserViewModel> Converter(List<User> users) {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var u in users) {
                userViewModels.Add((UserViewModel)Converter(u));
            }
            return userViewModels;
        }

        public static User Converter(UserViewModel userVM) {
            return new User() {
                idUser = userVM.idUser,
                dni = userVM.dni,
                name = userVM.name,
                lastName = userVM.lastName,
                email = userVM.email,
                password = userVM.password,
                rol = userVM.rol,
                height = userVM.height,
                weight = userVM.weight,
                active = userVM.active
            };
        }

        #region Coto
        public Assistance assistance { get; set; }
        #endregion

    }
}