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
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Propiedades> Propiedades { get; set; }
        public DbSet<Mejoras> Mejoras { get; set; }
        public DbSet<MejorasPropiedades> MejorasPropiedades { get; set; }
        public DbSet<TipoVentas> TipoVentas { get; set; }
        public DbSet<TipoPropiedades> TipoPropiedades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UsuariosMap());
            builder.ApplyConfiguration(new PropiedadesMap());
            builder.ApplyConfiguration(new MejorasMap());
            builder.ApplyConfiguration(new MejorasPropiedadesMap());
            builder.ApplyConfiguration(new TipoVentasMap());
            builder.ApplyConfiguration(new TipoPropiedadesMap());
        }
    }
}