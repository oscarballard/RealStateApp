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
using RealStateApp.Core.Application.ViewModels.ClientLike;
using RealStateApp.Core.Application.Features.Categories.Queries.GetAllCategories;
using RealStateApp.Core.Application.Features.Products.Queries.GetAllProducts;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using StockApp.Core.Application.Features.Categories.Commands.UpdateCategory;
using RealStateApp.Core.Application.Features.SalesType.Commands.UpdatePropertyType;
using RealStateApp.Core.Application.Features.SalesType.Queries.GetSalesTypeById;
using RealStateApp.Core.Application.Features.SalesType.Queries.GetAllSalesType;
using RealStateApp.Core.Application.Features.SalesType.Commands.CreateSalesType;
using RealStateApp.Core.Application.Features.Improvements.Commands.CreateImprovements;
using RealStateApp.Core.Application.Features.Improvements.Commands.UpdateImprovements;
using RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;

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

            CreateMap<RegisterRequest, SaveClientAgentViewModel>()
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
                .ForMember(dest => dest.MejorasId, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.TipoPropiedad, opt => opt.Ignore())
                .ForMember(dest => dest.TipoVenta, opt => opt.Ignore());

            CreateMap<Property, PropertyViewModel>()
                .ReverseMap();

            CreateMap<PropertyImprovements, SavePropertyImprovementsViewModel> ()
                .ReverseMap()
                .ForMember(dest => dest.Propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.Mejora, opt => opt.Ignore());

            CreateMap<SaveClientAgentViewModel, UsersViewModel>()
                .ForMember(x => x.Identification, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.Role, opt => opt.Ignore())
                .ForMember(x => x.Username, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore());

            CreateMap<PropertyImprovements, PropertyImprovementsViewModel>()
                .ReverseMap();

            CreateMap<ClientLike, SaveClientLikeViewModel>()
                .ReverseMap();


            #region Agents features
            CreateMap<GetAllAgentsQuery, GetAllAgentsParameter>()
                .ReverseMap();

            CreateMap<GetAllAgentsResponse, UsersViewModel>()
                .ForMember(x => x.Identification, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.Role, opt => opt.Ignore())
                .ForMember(x => x.Type, opt => opt.Ignore())
                .ForMember(x => x.Username, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, UpdateAgentsCommand>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.FirstName, opt => opt.Ignore())
                .ForMember(x => x.LastName, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Amount, opt => opt.Ignore())
                .ForMember(x => x.RolId, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore());
            #endregion
            
            
            #region FeaturesSalesType
            CreateMap<SalesTypeViewModel, UpdateSalesTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.CantPropiedades, opt => opt.Ignore());

            CreateMap<SalesType, CreateSalesTypeCommand>()
                .ReverseMap();

            CreateMap<SalesType, UpdateSalesTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Propiedades, opt => opt.Ignore());

            CreateMap<PropertyTypeViewModel, UpdatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.CantPropiedades, opt => opt.Ignore());

            CreateMap<PropertyType, CreatePropertyTypeCommand>()
                .ReverseMap();

            CreateMap<PropertyType, UpdatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Propiedades, opt => opt.Ignore());

            CreateMap<ImprovementsViewModel, UpdateImprovementsCommand>()
                .ReverseMap()
                .ForMember(x => x.CantPropiedades, opt => opt.Ignore());

            CreateMap<Improvements, CreateImprovementsCommand>()
                .ReverseMap();

            CreateMap<Improvements, UpdateImprovementsCommand>()
                .ReverseMap()
                .ForMember(x => x.Propiedades, opt => opt.Ignore());
            #endregion
        }
    }
}
