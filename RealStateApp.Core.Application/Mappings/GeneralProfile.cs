using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.SalesType;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyImprovements;

namespace RealStateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUsersViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PropertyType, SavePropertyTypeViewModel>()
               .ReverseMap();

            CreateMap<PropertyType, PropertyTypeViewModel>()
                .ForMember(dest => dest.CantPropiedades, opt => opt.Ignore())
                .ReverseMap(); 

            CreateMap<SalesType, SaveSalesTypeViewModel>()
                .ReverseMap();

            CreateMap<SalesType, SalesTypeViewModel>()
                .ForMember(dest => dest.CantPropiedades, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Improvements, SaveImprovementsViewModel>()
                .ReverseMap();

            CreateMap<Improvements, ImprovementsViewModel>()
                .ForMember(dest => dest.CantPropiedades, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<SaveClientAgentViewModel, SaveUsersViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.Identification, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Property, SavePropertyViewModel>()
                .ForMember(dest => dest.FileImagen1, opt => opt.Ignore())
                .ForMember(dest => dest.FileImagen2, opt => opt.Ignore())
                .ForMember(dest => dest.FileImagen3, opt => opt.Ignore())
                .ForMember(dest => dest.FileImagen4, opt => opt.Ignore())
                .ForMember(dest => dest.Mejoras, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.TipoPropiedad, opt => opt.Ignore())
                .ForMember(dest => dest.TipoVenta, opt => opt.Ignore())
                .ForMember(dest => dest.Mejoras, opt => opt.Ignore());

            CreateMap<Property, PropertyViewModel>()
                .ReverseMap();

            CreateMap<PropertyImprovements, SavePropertyImprovementsViewModel> ()
                .ReverseMap()
                .ForMember(dest => dest.Propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.Mejora, opt => opt.Ignore());

            CreateMap<PropertyImprovements, PropertyImprovementsViewModel>()
                .ReverseMap();
        }
    }
}
