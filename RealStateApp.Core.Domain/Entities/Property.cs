using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
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

        //NAVIGATION PROPERTTY
        public SalesType TipoVenta { get; set; }
        public PropertyType TipoPropiedad { get; set; }
        public Users Usuario { get; set; }
        public ICollection<Improvements> Mejoras { get; set; }
    }
}
