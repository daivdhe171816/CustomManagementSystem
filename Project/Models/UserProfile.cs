using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class UserProfile
{
    public int UserProfileId { get; set; }

    public int? UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Sex { get; set; }

    public string? PhoneNumber { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public virtual User? User { get; set; }
}
