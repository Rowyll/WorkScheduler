using System;
using System.Collections.Generic;

namespace WorkScheduler;

public partial class Worker
{
    public int WorkerId { get; set; }

    public string Fio { get; set; } = null!;

    public DateTime BirthDay { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int RoleId { get; set; }

    public int? SpecId { get; set; }

    public string? Program { get; set; }

    public int LocalCode { get; set; }

    public int GlobalCode { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual Specialization? Spec { get; set; }
}
