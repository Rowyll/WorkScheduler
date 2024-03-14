using System;
using System.Collections.Generic;

namespace WorkScheduler;

public partial class Specialization
{
    public int SpecId { get; set; }

    public string SpecName { get; set; } = null!;

    public bool? Permission { get; set; }

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();
}
