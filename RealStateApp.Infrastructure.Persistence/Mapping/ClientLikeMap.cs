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
    public class ClientLikeMap : IEntityTypeConfiguration<ClientLike>
    {
        public void Configure(EntityTypeBuilder<ClientLike> builder)
        {
            builder.ToTable("ClientLike")
                .HasKey(p => p.Id);
        }
    }
}
