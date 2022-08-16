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
    public class SalesTypeMap : IEntityTypeConfiguration<SalesType>
    {
        public void Configure(EntityTypeBuilder<SalesType> builder)
        {
            builder.ToTable("TipoVentas")
                .HasKey(u => u.Id);

            builder
                .HasMany<Property>(u => u.Propiedades)
                .WithOne(u => u.TipoVenta)
                .HasForeignKey(p => p.IdTipoVenta)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
