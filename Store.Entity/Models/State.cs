using System;
using System.Collections.Generic;

namespace Store.Entity.Models;

public partial class State
{
    public long StateId { get; set; }

    public long CountryId { get; set; }

    public string? StateName { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
}
