using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImprovements
    {
        public int Id { get; set; }
        public int IdPropiedad { get; set; }
        public int IdMejora { get; set; }

        public Property Propiedad { get; set; }
        public Improvements Mejora { get; set; }
    }
}
