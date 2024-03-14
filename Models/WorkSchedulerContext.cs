using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkScheduler;

public partial class WorkSchedulerContext : DbContext
{
    public WorkSchedulerContext()
    {
    }

    public WorkSchedulerContext(DbContextOptions<WorkSchedulerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=YUI;Database=WorkScheduler;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AA80467CC");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B616086FE9E89").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3214EC07D55A2337");

            entity.ToTable("Schedule");

            entity.HasOne(d => d.ShiftRoleNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ShiftRole)
                .HasConstraintName("FK__Schedule__ShiftR__5DCAEF64");

            entity.HasOne(d => d.Worker).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__Worker__5CD6CB2B");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecId).HasName("PK__Speciali__883D567B4E933C0D");

            entity.ToTable("Specialization");

            entity.HasIndex(e => e.SpecName, "UQ__Speciali__933C0142C06CC9B3").IsUnique();

            entity.Property(e => e.SpecName).HasMaxLength(100);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK__Worker__077C88263FC79705");

            entity.ToTable("Worker");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Worker__85FB4E380921641F").IsUnique();

            entity.HasIndex(e => e.GlobalCode, "UQ__Worker__BCDBB400218F0733").IsUnique();

            entity.HasIndex(e => e.Fio, "UQ__Worker__C1BEAA5C8CD01565").IsUnique();

            entity.Property(e => e.BirthDay).HasColumnType("date");
            entity.Property(e => e.Fio)
                .HasMaxLength(200)
                .HasColumnName("FIO");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Program).HasMaxLength(10);

            entity.HasOne(d => d.Role).WithMany(p => p.Workers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Worker__RoleId__403A8C7D");

            entity.HasOne(d => d.Spec).WithMany(p => p.Workers)
                .HasForeignKey(d => d.SpecId)
                .HasConstraintName("FK__Worker__SpecId__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
