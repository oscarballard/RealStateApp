﻿using RealStateApp.Core.Application.ViewModels.ClientLike;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.PropertyImprovements;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.SalesType;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdTipoPropiedad { get; set; }
        public int IdTipoVenta { get; set; }
        public float Precio { get; set; }
        public float Terreno { get; set; }
        public int CantHabitaciones { get; set; }
        public int CantLavabos { get; set; }
        public string Descripcion { get; set; }
        public string IdAgente { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }
        //public List<ClientLikeViewModel> ClientLikes { get; set; }

        //NAVIGATION PROPERTTY
        public SalesTypeViewModel TipoVenta { get; set; }
        public PropertyTypeViewModel TipoPropiedad { get; set; }
        public ICollection<ClientLikeViewModel> ClientLikes { get; set; }
        public UsersViewModel Usuario { get; set; }
        public ICollection<ImprovementsViewModel> Mejoras { get; set; }
    }
}
