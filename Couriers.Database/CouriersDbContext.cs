using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Couriers.Database.Models;

#nullable disable

namespace Couriers.Database
{
    public partial class CouriersDbContext : DbContext
    {
        public CouriersDbContext()
        {
        }

        public CouriersDbContext(DbContextOptions<CouriersDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobLine> JobLines { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleItem> ScheduleItems { get; set; }
        public virtual DbSet<ScheduleStatus> ScheduleStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-A2P25DT;Initial Catalog=Couriers;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JobStatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.JobStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_JobStatus");
            });

            modelBuilder.Entity<JobLine>(entity =>
            {
                entity.HasOne(d => d.CollectionAddress)
                    .WithMany(p => p.JobLineCollectionAddresses)
                    .HasForeignKey(d => d.CollectionAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobLine_Address");

                entity.HasOne(d => d.DeliveryAddress)
                    .WithMany(p => p.JobLineDeliveryAddresses)
                    .HasForeignKey(d => d.DeliveryAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobLine_Address1");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobLines)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobLine_Job");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleStatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ScheduleStatus)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ScheduleStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_ScheduleStatus");
            });

            modelBuilder.Entity<ScheduleItem>(entity =>
            {
                entity.HasOne(d => d.JobLine)
                    .WithMany(p => p.ScheduleItems)
                    .HasForeignKey(d => d.JobLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleItems_JobLine");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ScheduleItems)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleItems_Schedule");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
