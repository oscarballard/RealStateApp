using RealStateApp.Core.Application.ViewModels.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllAgentsResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PropertiesCount  { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
