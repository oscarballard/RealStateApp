using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class SaveClientAgentViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar su nombre ")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar su apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

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

        [Required(ErrorMessage = "Debe colocar un teléfono")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        public RolesViewModel Roles { get; set; }
        public List<RolesViewModel> RolesList { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
