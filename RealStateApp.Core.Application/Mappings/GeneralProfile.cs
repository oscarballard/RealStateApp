﻿using AutoMapper;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.SalesType;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
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

        }
    }
}
