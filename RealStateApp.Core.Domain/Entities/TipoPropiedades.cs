﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class TipoPropiedades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //NAVIGATION PROPERTTY
        public ICollection<Propiedades> Propiedades { get; set; }
    }
}
