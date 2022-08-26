using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Mapping;
//using RealStateApp.Core.Domain.Entities;
//using RealStateApp.Infrastructure.Persistence.Mapping;

namespace RealStateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Property> Propiedades { get; set; }
        public DbSet<Improvements> Mejoras { get; set; }
        public DbSet<PropertyImprovements> MejorasPropiedades { get; set; }
        public DbSet<SalesType> TipoVentas { get; set; }
        public DbSet<PropertyType> TipoPropiedades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PropertyMap());
            builder.ApplyConfiguration(new ImprovementsMap());
            builder.ApplyConfiguration(new PropertyImprovementsMap());
            builder.ApplyConfiguration(new SalesTypeMap());
            builder.ApplyConfiguration(new PropertyTypeMap());
            builder.ApplyConfiguration(new ClientLikeMap());
        }
    }
}