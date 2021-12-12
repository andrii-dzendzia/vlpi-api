﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vlpi.Data.Models;

namespace Vlpi.Data.Migrations
{
    [DbContext(typeof(VLPIContext))]
    partial class VLPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vlpi.Data.Models.AnswerBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BetterAnswerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex("BetterAnswerId");

                    b.HasIndex(new[] { "Id" }, "XIF1AnswerBlock");

                    b.HasIndex(new[] { "TaskId" }, "XIF2AnswerBlock");

                    b.HasIndex(new[] { "Id" }, "XPKAnswerBlock")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("AnswerBlock");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Emoji", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex(new[] { "Id" }, "XPKEmoji")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("Emoji");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Testing enabled"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Testing disabled"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Development enabled"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Development disabled"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Design enabled"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Design disabled"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Project management enabled"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Project management disabled"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Brain"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Eyes"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Note"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Student"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Win"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Requirements analysis enabled"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Requirements analysis disabled"
                        });
                });

            modelBuilder.Entity("Vlpi.Data.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex(new[] { "Id" }, "XPKModule")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("Module");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsEnabled = true,
                            Name = "Testing"
                        },
                        new
                        {
                            Id = 2,
                            IsEnabled = false,
                            Name = "Development"
                        },
                        new
                        {
                            Id = 3,
                            IsEnabled = false,
                            Name = "Design"
                        },
                        new
                        {
                            Id = 4,
                            IsEnabled = false,
                            Name = "Project management"
                        },
                        new
                        {
                            Id = 5,
                            IsEnabled = false,
                            Name = "Requirements analysis"
                        });
                });

            modelBuilder.Entity("Vlpi.Data.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex(new[] { "TestId" }, "XIF1Task");

                    b.HasIndex(new[] { "Id" }, "XPKTask")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("Task");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex(new[] { "ModuleId" }, "XIF1Test");

                    b.HasIndex(new[] { "AdminId" }, "XIF2Test");

                    b.HasIndex(new[] { "Id" }, "XPKTest")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Vlpi.Data.Models.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsInProgress")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex(new[] { "TestId" }, "XIF1TestResult");

                    b.HasIndex(new[] { "UserId" }, "XIF2TestResult");

                    b.HasIndex(new[] { "Id" }, "XPKTestResult")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("TestResult");
                });

            modelBuilder.Entity("Vlpi.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Studying")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Workplase")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex(new[] { "Id" }, "XPKUser")
                        .IsUnique()
                        .IsClustered();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Vlpi.Data.Models.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("TestResultId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TestResultId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("Vlpi.Data.Models.AnswerBlock", b =>
                {
                    b.HasOne("Vlpi.Data.Models.AnswerBlock", "BetterAnswer")
                        .WithMany("InverseBetterAnswer")
                        .HasForeignKey("BetterAnswerId")
                        .HasConstraintName("R_1");

                    b.HasOne("Vlpi.Data.Models.Task", "Task")
                        .WithMany("AnswerBlocks")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("R_2")
                        .IsRequired();

                    b.Navigation("BetterAnswer");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Task", b =>
                {
                    b.HasOne("Vlpi.Data.Models.Test", "Test")
                        .WithMany("Tasks")
                        .HasForeignKey("TestId")
                        .HasConstraintName("R_9")
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Test", b =>
                {
                    b.HasOne("Vlpi.Data.Models.User", "Admin")
                        .WithMany("Tests")
                        .HasForeignKey("AdminId")
                        .HasConstraintName("R_11")
                        .IsRequired();

                    b.HasOne("Vlpi.Data.Models.Module", "Module")
                        .WithMany("Tests")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("R_8")
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Vlpi.Data.Models.TestResult", b =>
                {
                    b.HasOne("Vlpi.Data.Models.Test", "Test")
                        .WithMany("TestResults")
                        .HasForeignKey("TestId")
                        .HasConstraintName("R_5")
                        .IsRequired();

                    b.HasOne("Vlpi.Data.Models.User", "User")
                        .WithMany("TestResults")
                        .HasForeignKey("UserId")
                        .HasConstraintName("R_6")
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vlpi.Data.Models.UserAnswer", b =>
                {
                    b.HasOne("Vlpi.Data.Models.Task", "Task")
                        .WithMany("UserAnswers")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("R_4")
                        .IsRequired();

                    b.HasOne("Vlpi.Data.Models.TestResult", "TestResult")
                        .WithMany("UserAnswers")
                        .HasForeignKey("TestResultId")
                        .HasConstraintName("FK_UserAnswer_TestResult")
                        .IsRequired();

                    b.HasOne("Vlpi.Data.Models.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("R_7")
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("TestResult");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vlpi.Data.Models.AnswerBlock", b =>
                {
                    b.Navigation("InverseBetterAnswer");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Module", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Task", b =>
                {
                    b.Navigation("AnswerBlocks");

                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("Vlpi.Data.Models.Test", b =>
                {
                    b.Navigation("Tasks");

                    b.Navigation("TestResults");
                });

            modelBuilder.Entity("Vlpi.Data.Models.TestResult", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("Vlpi.Data.Models.User", b =>
                {
                    b.Navigation("TestResults");

                    b.Navigation("Tests");

                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
