using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class FilterPropertyViewModel
    {
        public int? Tipo { get; set; }
        public float? MinPrecio { get; set; }
        public float? MaxPrecio { get; set; }
        public int? CantHabitaciones { get; set; }
        public int? CantLavabos { get; set; }
        public string? IdAgent { get; set; }
        public string? IdClient { get; set; }
        public string? Codigo { get; set; }
    }
}
