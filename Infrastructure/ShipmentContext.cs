using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure
{
    public partial class ShipmentContext : DbContext
    {
        public ShipmentContext()
        {
        }

        public ShipmentContext(DbContextOptions<ShipmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONN_STRING"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("organization");

                entity.HasIndex(e => e.Code, "org_code_index");

                entity.Ignore(e => e.Type);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("code");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.ReferenceId)
                    .HasName("shipment_pkey");

                entity.ToTable("shipment");
                
                entity.Ignore(e => e.Type);

                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(50)
                    .HasColumnName("reference_id");

                entity.Property(e => e.EstimatedTimeArrival)
                    .HasColumnType("date")
                    .HasColumnName("estimated_time_arrival");

                entity.Property(e => e.Organizations)
                    .HasColumnType("jsonb")
                    .HasColumnName("organizations");

                entity.Property(e => e.TransportPacks)
                    .HasColumnType("jsonb")
                    .HasColumnName("transport_packs");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
