using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Url_RAP_checker.Models.Db
{
    public partial class URLContext : DbContext
    {
        public URLContext()
        {
        }

        public URLContext(DbContextOptions<URLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TimeCounter> TimeCounter { get; set; }
        public virtual DbSet<Url> Url { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=URL;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeCounter>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Count).HasColumnName("count");
            });

            modelBuilder.Entity<Url>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IntervalCheck).HasMaxLength(50);

                entity.Property(e => e.LastTimeCheck).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ResultCheck).HasMaxLength(300);

                entity.Property(e => e.StatuseCheck).HasMaxLength(50);

                entity.Property(e => e.UrlCheck)
                    .HasColumnName("urlCheck")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PassWord).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
