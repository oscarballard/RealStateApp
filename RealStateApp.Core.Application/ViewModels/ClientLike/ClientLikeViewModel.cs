using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.ClientLike
{
    public class ClientLikeViewModel
    {
        public int Id { get; set; }
        public int IdPropiedad { get; set; }
        public string IdClient { get; set; }
        public UsersViewModel User { get; set; }
        public ImprovementsViewModel Mejora { get; set; }
    }
}
