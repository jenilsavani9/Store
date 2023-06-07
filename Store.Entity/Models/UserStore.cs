using System;
using System.Collections.Generic;

namespace Store.Entity.Models;

public partial class UserStore
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

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
