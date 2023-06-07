using System;
using System.Collections.Generic;

namespace Store.Entity.Models;

public partial class City
{
    public long CityId { get; set; }

    public long CountryId { get; set; }

    public long StateId { get; set; }

    public string? CityName { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
}
