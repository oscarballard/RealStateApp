using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyImprovements;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class PropertyImprovementsService : GenericService<SavePropertyImprovementsViewModel, PropertyImprovementsViewModel, PropertyImprovements>, IPropertyImprovementsService
    {
        private readonly IPropertyImprovementsRepository _propertyImprovementsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public PropertyImprovementsService(IPropertyImprovementsRepository propertyImprovementsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(propertyImprovementsRepository, mapper)
        {
            _propertyImprovementsRepository = propertyImprovementsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
    }
}
