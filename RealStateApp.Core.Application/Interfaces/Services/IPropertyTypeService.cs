using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyTypeService : IGenericService<SavePropertyTypeViewModel, PropertyTypeViewModel, PropertyType>
    {
        Task<List<PropertyTypeViewModel>> GetAllWithIncludes();
    }
}
