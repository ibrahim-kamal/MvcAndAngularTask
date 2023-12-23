using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;

namespace MvcAndAngularTask.Models;

public class Patient
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Gender { get; set; }

    public string? NationalId { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();


}
