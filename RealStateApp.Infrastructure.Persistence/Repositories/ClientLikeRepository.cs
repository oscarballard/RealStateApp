using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;
using RealStateApp.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class ClientLikeRepository : GenericRepository<ClientLike>, IClientLikeRepository
    {
        private readonly ApplicationContext _dbContext;
        public ClientLikeRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Dislike(int IdPropiedad, string IdUser)
        {
            _dbContext.Set<ClientLike>().Where(p => p.IdPropiedad == IdPropiedad).Where(p => p.IdClient == IdUser)
              .ToList().ForEach(p => _dbContext.Set<ClientLike>().Remove(p));
            await _dbContext.SaveChangesAsync();
        }
    }
}
