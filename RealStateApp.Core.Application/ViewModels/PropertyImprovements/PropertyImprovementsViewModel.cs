using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropertyImprovements
{
    public class PropertyImprovementsViewModel
    {
        public int Id { get; set; }
        public int IdPropiedad { get; set; }
        public int IdMejora { get; set; }
        public PropertyViewModel Propiedad { get; set; }
        public ImprovementsViewModel Mejora { get; set; }
    }
}
