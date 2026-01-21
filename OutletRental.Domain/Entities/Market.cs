using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Domain.Entities
{
    public class Market
    {
        public string Id { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public List<string> AllowedVehicleCategories { get; set; } = new();
    }
}
