using System;
using System.Collections.Generic;

namespace Store.Entity.Models;

public partial class Country
{
    public long CountryId { get; set; }

    public string? CountryName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
}
