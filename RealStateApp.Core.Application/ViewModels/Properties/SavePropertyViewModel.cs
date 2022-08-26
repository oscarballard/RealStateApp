﻿using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.ViewModels.Improvements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class SavePropertyViewModel
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
        public List<ImprovementsViewModel> Mejoras { get; set; }
        public List<int> MejorasId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile FileImagen1 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile FileImagen2 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile FileImagen3 { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile FileImagen4 { get; set; }

    }
}
