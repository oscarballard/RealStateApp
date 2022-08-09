using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class MejorasPropiedades
    {
        public int Id { get; set; }
        public int IdPropiedad { get; set; }
        public int IdMejora { get; set; }

        public Propiedades Propiedad { get; set; }
        public Mejoras Mejora { get; set; }
    }
}
