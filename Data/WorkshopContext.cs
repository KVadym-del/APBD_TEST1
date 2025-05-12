using Microsoft.EntityFrameworkCore;
using APBD_TEST1.Models;

namespace APBD_TEST1.Data
{
    public class WorkshopContext : DbContext
    {
        public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<VisitService> VisitServices => Set<VisitService>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");
                entity.HasKey(e => e.ClientId);
                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            });

            modelBuilder.Entity<Mechanic>(entity =>
            {
                entity.ToTable("Mechanic");
                entity.HasKey(e => e.MechanicId);
                entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.LicenceNumber).HasColumnName("licence_number");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.BaseFee).HasColumnName("base_fee");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("Visit");
                entity.HasKey(e => e.VisitId);
                entity.Property(e => e.VisitId).HasColumnName("visit_id");
                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
                entity.Property(e => e.Date).HasColumnName("date");
            });

            modelBuilder.Entity<VisitService>(entity =>
            {
                entity.ToTable("Visit_Service");
                entity.HasKey(e => new { e.ServiceId, e.VisitId });
                entity.Property(e => e.VisitId).HasColumnName("visit_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.ServiceFee).HasColumnName("service_fee");
            });

            modelBuilder.Entity<VisitService>()
                .HasOne(vs => vs.Visit)
                .WithMany(v => v.VisitServices)
                .HasForeignKey(vs => vs.VisitId);

            modelBuilder.Entity<VisitService>()
                .HasOne(vs => vs.Service)
                .WithMany(s => s.VisitServices)
                .HasForeignKey(vs => vs.ServiceId);
        }
    }
}