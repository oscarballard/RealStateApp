using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence.Mapping
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure( EntityTypeBuilder<Usuarios> builder) 
        {
            builder.ToTable("Usuarios")
                .HasKey(u => u.Id);

            builder
                .HasMany<Propiedades>(u => u.Propiedades)
                .WithOne(u => u.Usuario)
                .HasForeignKey(p => p.IdAgente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
