using System;
using System.Collections.Generic;

namespace WorkScheduler;

public partial class Schedule
{
    public int Id { get; set; }

    public int WorkerId { get; set; }

    public int ShiftDay { get; set; }

    public TimeSpan ShiftStart { get; set; }

    public TimeSpan ShiftEnd { get; set; }

    public int? ShiftRole { get; set; }

    public virtual Specialization? ShiftRoleNavigation { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
