using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.SalesType;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class SalesTypeService : GenericService<SaveSalesTypeViewModel, SalesTypeViewModel,SalesType>, ISalesTypeService
    {
        private readonly ISalesTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsersViewModel userViewModel;
        public SalesTypeService(ISalesTypeRepository propertyTypeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(propertyTypeRepository , mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsersViewModel>("user");
        }

        public async Task<List<SalesTypeViewModel>> GetAllWithIncludes()
        {
            var amigosList = await _propertyTypeRepository.GetAllWithIncludeAsync(new List<string> { "Propiedades" });

            return amigosList.Select(post => new SalesTypeViewModel
            {
                Id = post.Id,
                Nombre = post.Nombre,
                Descripcion = post.Descripcion,
                CantPropiedades = post.Propiedades.Count()
            }).ToList();
        }
    }
}
