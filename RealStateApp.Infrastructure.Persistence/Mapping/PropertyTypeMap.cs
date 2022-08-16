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
    public class PropertyTypeMap : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.ToTable("TipoPropiedades")
                .HasKey(u => u.Id);

            builder
                .HasMany<Property>(u => u.Propiedades)
                .WithOne(u => u.TipoPropiedad)
                .HasForeignKey(p => p.IdTipoPropiedad)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
