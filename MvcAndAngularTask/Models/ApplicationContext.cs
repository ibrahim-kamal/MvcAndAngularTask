using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MvcAndAngularTask.Models.DTO;

namespace MvcAndAngularTask.Models;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }
    public virtual DbSet<ReservationcountByDoctorDto> ReservationcountByDoctorDto { get; set; }

    //scaffold-dbcontext 'Data Source=DESKTOP-5A0QPBF;Initial Catalog=MvcAndAngularTask;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False' Microsoft.EntityFrameworkCore.sqlserver -outputDir Models -context ApplicationContext
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__doctors__3213E83F67BC3248");

            entity.ToTable("doctors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DoctorId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("doctorId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patient__3213E83F05DDEF9C");

            entity.ToTable("patient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NationalId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nationalId");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reservat__3213E83F65EDB6AB");

            entity.ToTable("reservation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.To).HasColumnName("to");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__reservati__docto__3E52440B");

            entity.HasOne(d => d.Patient).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__reservati__patie__3F466844");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F767D2EB9");

            entity.ToTable("schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Day)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("day");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.To).HasColumnName("to");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__schedule__doctor__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
