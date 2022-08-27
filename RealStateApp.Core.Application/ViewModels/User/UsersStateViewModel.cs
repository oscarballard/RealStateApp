using RealStateApp.Core.Application.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class UsersStateViewModel
    {
        public UsersStateByRolViewModel Admin { get; set; }
        public UsersStateByRolViewModel Agent { get; set; }
        public UsersStateByRolViewModel Dev { get; set; }
        public UsersStateByRolViewModel Client { get; set; }
    }
}
