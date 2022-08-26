using RealStateApp.Core.Application.ViewModels.ClientLike;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyImprovements;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IClientLikeService : IGenericService<SaveClientLikeViewModel, ClientLikeViewModel, ClientLike>
    {
        Task<SaveClientLikeViewModel> Add(int IdPropiedad);
        Task<List<ClientLikeViewModel>> GetByPropertyAndClient(int Id);
        Task  Dislike(List<ClientLikeViewModel> Likes);
    }
}
