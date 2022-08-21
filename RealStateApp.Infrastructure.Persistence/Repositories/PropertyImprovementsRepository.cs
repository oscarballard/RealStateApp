using RealStateApp.Core.Application.Interfaces.Repositories;
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
    public class PropertyImprovementsRepository : GenericRepository<PropertyImprovements>, IPropertyImprovementsRepository
    {
        private readonly ApplicationContext _dbContext;
        public PropertyImprovementsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAllAsync(int IdPropiedad)
        {
            _dbContext.Set<PropertyImprovements>().Where(p => p.IdPropiedad == IdPropiedad)
              .ToList().ForEach(p => _dbContext.Set<PropertyImprovements>().Remove(p));
            await _dbContext.SaveChangesAsync();
        }
    }
}
