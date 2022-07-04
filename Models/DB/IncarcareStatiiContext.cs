using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StatiiIncarcare.Models.DB
{
    public partial class IncarcareStatiiContext : DbContext
    {
        public IncarcareStatiiContext()
        {
        }

        public IncarcareStatiiContext(DbContextOptions<IncarcareStatiiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Priza> Prizas { get; set; } = null!;
        public virtual DbSet<Rezervare> Rezervares { get; set; } = null!;
        public virtual DbSet<Statii> Statiis { get; set; } = null!;
        public virtual DbSet<Tip> Tips { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
     


        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=IncarcareStatii;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Priza>(entity =>
            {
                entity.HasKey(e => e.IdPriza);

                entity.ToTable("Priza");

                entity.HasOne(d => d.IdStatieNavigation)
                    .WithMany(p => p.Prizas)
                    .HasForeignKey(d => d.IdStatie)
                    .HasConstraintName("FK_Priza_Statii");

                entity.HasOne(d => d.IdTipNavigation)
                    .WithMany(p => p.Prizas)
                    .HasForeignKey(d => d.IdTip)
                    .HasConstraintName("FK_Priza_Tip");
            });

            modelBuilder.Entity<Rezervare>(entity =>
            {
                entity.HasKey(e => e.IdRezervare);

                entity.ToTable("Rezervare");

                entity.Property(e => e.IdRezervare).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NrMasina).HasMaxLength(50);

                entity.Property(e => e.TimeIn).HasColumnType("datetime");

                entity.Property(e => e.TimeOut).HasColumnType("datetime");

                entity.HasOne(d => d.IdPrizaNavigation)
                    .WithMany(p => p.Rezervares)
                    .HasForeignKey(d => d.IdPriza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rezervare_Priza1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Rezervares)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Rezervare_Users");
            });

            modelBuilder.Entity<Statii>(entity =>
            {
                entity.HasKey(e => e.IdStatie);

                entity.ToTable("Statii");

                entity.Property(e => e.Adresa).HasMaxLength(50);

                entity.Property(e => e.Nume).HasMaxLength(50);

                entity.Property(e => e.Oras).HasMaxLength(50);
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.HasKey(e => e.IdTip);

                entity.ToTable("Tip");

                entity.Property(e => e.Nume).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.Nume).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
