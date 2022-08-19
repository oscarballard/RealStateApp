using RealStateApp.Core.Application.ViewModels.SalesType;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface ISalesTypeService : IGenericService<SaveSalesTypeViewModel, SalesTypeViewModel, SalesType>
    {
        Task<List<SalesTypeViewModel>> GetAllWithIncludes();
    }
}
