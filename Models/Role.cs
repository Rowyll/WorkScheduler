using System;
using System.Collections.Generic;

namespace WorkScheduler;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();
}
