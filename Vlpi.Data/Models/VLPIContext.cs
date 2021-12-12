using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class VLPIContext : DbContext
    {
        public VLPIContext(DbContextOptions<VLPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswerBlock> AnswerBlocks { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AnswerBlock>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("AnswerBlock");

                entity.HasIndex(e => e.Id, "XIF1AnswerBlock");

                entity.HasIndex(e => e.TaskId, "XIF2AnswerBlock");

                entity.HasIndex(e => e.Id, "XPKAnswerBlock")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.BetterAnswer)
                    .WithMany(p => p.InverseBetterAnswer)
                    .HasForeignKey(d => d.BetterAnswerId)
                    .HasConstraintName("R_1");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.AnswerBlocks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_2");
            });

            modelBuilder.Entity<Emoji>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Emoji");

                entity.HasIndex(e => e.Id, "XPKEmoji")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Module");

                entity.HasIndex(e => e.Id, "XPKModule")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Task");

                entity.HasIndex(e => e.TestId, "XIF1Task");

                entity.HasIndex(e => e.Id, "XPKTask")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Answer).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_9");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Test");

                entity.HasIndex(e => e.ModuleId, "XIF1Test");

                entity.HasIndex(e => e.AdminId, "XIF2Test");

                entity.HasIndex(e => e.Id, "XPKTest")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_8");

                entity.HasOne(d => d.Admin)
                    .WithMany(a => a.Tests)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_11");
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("TestResult");

                entity.HasIndex(e => e.TestId, "XIF1TestResult");

                entity.HasIndex(e => e.UserId, "XIF2TestResult");

                entity.HasIndex(e => e.Id, "XPKTestResult")
                    .IsUnique()
                    .IsClustered();

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_6");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("User");

                entity.HasIndex(e => e.Id, "XPKUser")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Studying).HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Workplase).HasMaxLength(50);
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.ToTable("UserAnswer");

                entity.Property(e => e.Answer).IsRequired();

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_4");

                entity.HasOne(d => d.TestResult)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.TestResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAnswer_TestResult");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_7");
            });

            modelBuilder.Entity<Module>().HasData(
                new Module { Id = 1, IsEnabled = true, Name = "Testing" },
                new Module { Id = 2, IsEnabled = false, Name = "Development" },
                new Module { Id = 3, IsEnabled = false, Name = "Design" },
                new Module { Id = 4, IsEnabled = false, Name = "Project management" },
                new Module { Id = 5, IsEnabled = false, Name = "Requirements analysis" });

            modelBuilder.Entity<Emoji>().HasData(
                new Emoji { Id = 1, Name = "Testing enabled" },
                new Emoji { Id = 2, Name = "Testing disabled" },
                new Emoji { Id = 3, Name = "Development enabled" },
                new Emoji { Id = 4, Name = "Development disabled" },
                new Emoji { Id = 5, Name = "Design enabled" },
                new Emoji { Id = 6, Name = "Design disabled" },
                new Emoji { Id = 7, Name = "Project management enabled" },
                new Emoji { Id = 8, Name = "Project management disabled" },
                new Emoji { Id = 9, Name = "Brain" },
                new Emoji { Id = 10, Name = "Eyes" },
                new Emoji { Id = 11, Name = "Note" },
                new Emoji { Id = 12, Name = "Student" },
                new Emoji { Id = 13, Name = "Win" },
                new Emoji { Id = 14, Name = "Requirements analysis enabled" },
                new Emoji { Id = 15, Name = "Requirements analysis disabled" });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
