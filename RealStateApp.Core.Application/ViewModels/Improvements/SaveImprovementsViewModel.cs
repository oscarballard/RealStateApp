using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Improvements
{
    public class SaveImprovementsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe Colocar un Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe Colocar una Descripcion")]
        public string Descripcion { get; set; }
    }
}
