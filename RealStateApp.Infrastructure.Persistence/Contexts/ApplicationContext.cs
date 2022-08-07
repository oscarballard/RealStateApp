using Microsoft.EntityFrameworkCore;
//using RealStateApp.Core.Domain.Entities;
//using RealStateApp.Infrastructure.Persistence.Mapping;

namespace RealStateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}