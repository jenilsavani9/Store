 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class StoresModel
    {
        public int StoreId { get; set; }

        public int UserId { get; set; }

        public string? StoreName { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public string? PostalCode { get; set; }

        public string? LocationLink { get; set; }

        public string Status { get; set; } = null!;
    }
}
