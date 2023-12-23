using MvcAndAngularTask.Controllers;
using System;
using System.Collections.Generic;

namespace MvcAndAngularTask.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public DateTime? Date { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }

}
