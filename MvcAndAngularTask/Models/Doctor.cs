using MvcAndAngularTask.Controllers;
using System;
using System.Collections.Generic;

namespace MvcAndAngularTask.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? DoctorId { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    
}
