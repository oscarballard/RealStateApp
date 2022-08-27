using RealStateApp.Core.Application.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class UsersStateByRolViewModel
    {
        public int UserActive { get; set; }
        public int UserInactive { get; set; }
    }
}
