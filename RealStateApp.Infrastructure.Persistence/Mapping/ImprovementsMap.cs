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
    public class ImprovementsMap : IEntityTypeConfiguration<Improvements>
    {
        public void Configure(EntityTypeBuilder<Improvements> builder)
        {
            builder.ToTable("Mejoras")
                .HasKey(u => u.Id);
        }
    }
}
