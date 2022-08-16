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
    class PropertyMap : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Propiedades")
                .HasKey(p => p.Id);

            builder
                .HasMany(p => p.Mejoras)
                .WithMany(p => p.Propiedades)
                .UsingEntity<PropertyImprovements>(
                    mp => mp.HasOne(prop => prop.Mejora)
                    .WithMany()
                    .HasForeignKey(prop => prop.IdMejora),
                    mp => mp.HasOne(prop => prop.Propiedad)
                    .WithMany()
                    .HasForeignKey(prop => prop.IdPropiedad),
                    mp =>
                    {
                        mp.HasKey(prop => prop.Id);
                    }
                );
        }
    }
}
