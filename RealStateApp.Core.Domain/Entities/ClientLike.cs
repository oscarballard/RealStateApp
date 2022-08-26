using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class ClientLike
    {
        public int Id { get; set; }
        public int IdPropiedad { get; set; }
        public string IdClient { get; set; }

        public Property Propiedades { get; set; }
    }
}
