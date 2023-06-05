 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class StoresModel
    {
        public long StoreId { get; set; }

        public long UserId { get; set; }

        public string? StoreName { get; set; }

        public string? Address { get; set; }

        public long CountryId { get; set; }

        public long StateId { get; set; }

        public long CityId { get; set; }

        public int? PostalCode { get; set; }

        public string? LocationLink { get; set; }

        public string Status { get; set; } = null!;
    }
}
