using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<SavePropertyViewModel, PropertyViewModel, Property>
    {
        Task<List<PropertyViewModel>> GetAllViewModelWithFilters(FilterPropertyViewModel filters);
        Task<PropertyViewModel> GetByIdViewModel(int Id);
    }
}
