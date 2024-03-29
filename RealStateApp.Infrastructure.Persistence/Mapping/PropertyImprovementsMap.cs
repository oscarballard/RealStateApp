﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence.Mapping
{
    public class PropertyImprovementsMap : IEntityTypeConfiguration<PropertyImprovements>
    {
        public void Configure(EntityTypeBuilder<PropertyImprovements> builder)
        {
            builder.ToTable("MejorasPropiedades")
                .HasKey(p => p.Id);
        }
    }
}
