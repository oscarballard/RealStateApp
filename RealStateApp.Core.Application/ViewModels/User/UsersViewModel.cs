using RealStateApp.Core.Application.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Identification { get; set; }
        public IList<string> Type { get; set; }
        public string Phone { get; set; }
        public List<string> Roles { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
