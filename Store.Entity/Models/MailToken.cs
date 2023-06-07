using System;
using System.Collections.Generic;

namespace Store.Entity.Models;

public partial class MailToken
{
    public long MailTokenId { get; set; }

    public long UserId { get; set; }

    public string? Token { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
