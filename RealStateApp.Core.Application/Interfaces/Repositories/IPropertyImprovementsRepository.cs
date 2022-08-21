﻿using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropertyImprovementsRepository : IGenericRepository<PropertyImprovements>
    {
        Task DeleteAllAsync(int IdPropiedad);
    }
}
