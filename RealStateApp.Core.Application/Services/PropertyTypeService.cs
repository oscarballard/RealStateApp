using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
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
    public class PropertyTypeService : GenericService<SavePropertyTypeViewModel, PropertyTypeViewModel,PropertyType>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsersViewModel userViewModel;
        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(propertyTypeRepository , mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsersViewModel>("user");
        }

        public async Task<List<PropertyTypeViewModel>> GetAllWithIncludes()
        {
            var amigosList = await _propertyTypeRepository.GetAllWithIncludeAsync(new List<string> { "Propiedades" });

            return amigosList.Select(post => new PropertyTypeViewModel
            {
                Id = post.Id,
                Nombre = post.Nombre,
                Descripcion = post.Descripcion,
                CantPropiedades = post.Propiedades.Count()
            }).ToList();
        }
    }
}
