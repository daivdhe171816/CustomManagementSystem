using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
