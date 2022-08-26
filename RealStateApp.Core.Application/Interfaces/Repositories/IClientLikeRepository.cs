using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IClientLikeRepository : IGenericRepository<ClientLike>
    {
        Task Dislike(int IdPropiedad, string IdUser);
    }
}
