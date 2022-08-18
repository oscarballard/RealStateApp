using RealStateApp.Core.Application.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        public int State { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar su apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar su número de cedula")]
        [DataType(DataType.Text)]
        public string Identification { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una foto de usuario")]
        [DataType(DataType.Text)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Debe colocar un rol al usuario")]
        [DataType(DataType.Text)]
        public string RolId { get; set; }

        [Required(ErrorMessage = "Debe colocar un teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Este número no es válido")]
        public string Phone { get; set; }

        public float Amount { get; set; }

        public RolesViewModel Roles { get; set; }
        [Required(ErrorMessage = "Debe colocar un tipo de usuario")]
        [DataType(DataType.Text)]
        public string UserType { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
