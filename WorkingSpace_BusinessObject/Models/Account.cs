using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Account
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
