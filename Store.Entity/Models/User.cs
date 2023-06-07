﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Entity.Models;

public partial class User
{
    public long UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    [ForeignKey("Roles")]
    public int Roles { get; set; }

    public string Status { get; set; } = null!;

    public string? LastLogin { get; set; } = null;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<MailToken> MailTokens { get; set; } = new List<MailToken>();

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();

}
