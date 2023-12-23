using System;
using System.Collections.Generic;

namespace MvcAndAngularTask.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int? DoctorId { get; set; }

    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public string? Day { get; set; }

    public virtual Doctor? Doctor { get; set; }

}
