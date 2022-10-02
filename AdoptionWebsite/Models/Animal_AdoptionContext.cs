using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace AdoptionWebsite.Models
{
    public partial class Animal_AdoptionContext : DbContext
    {
        public Animal_AdoptionContext()
        {
        }

        public Animal_AdoptionContext(DbContextOptions<Animal_AdoptionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<AnimalCate> AnimalCate { get; set; }
        public virtual DbSet<Files> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

                var connectionString = configuration.GetConnectionString("AdoptionDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Idno);

                entity.Property(e => e.Idno).HasColumnName("IDNo");

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.Property(e => e.IsAdoption).HasDefaultValueSql("((0))");

                entity.Property(e => e.Memo).HasMaxLength(550);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Sex).HasMaxLength(10);
            });

            modelBuilder.Entity<AnimalCate>(entity =>
            {
                entity.HasKey(e => e.Idno);

                entity.ToTable("Animal_Cate");

                entity.Property(e => e.Idno).HasColumnName("IDNo");

                entity.Property(e => e.CateName).HasMaxLength(50);
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.Idno);

                entity.Property(e => e.Idno).HasColumnName("IDNo");

                entity.Property(e => e.FileName).HasMaxLength(150);

                entity.Property(e => e.FilePath).HasMaxLength(550);

                entity.Property(e => e.TableId).HasColumnName("Table_ID");

                entity.Property(e => e.XTable)
                    .HasColumnName("xTable")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
