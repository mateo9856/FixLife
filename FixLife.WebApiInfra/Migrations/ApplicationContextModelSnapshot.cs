﻿// <auto-generated />
using System;
using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FixLife.WebApiInfra.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.FreeTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TimeEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeStart")
                        .HasColumnType("time");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("FreeTimes");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.LearnTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsWeekly")
                        .HasColumnType("bit");

                    b.Property<bool>("IsYearly")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeInterval")
                        .HasColumnType("time");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LearnTimes");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LearnTimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WeeklyWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LearnTimeId");

                    b.HasIndex("WeeklyWorkId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.UserPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PlansId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlansId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserPlan");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.WeeklyWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayOfWeeks")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TimeEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeStart")
                        .HasColumnType("time");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WeeklyWorks");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.User.ClientUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClientUser");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.FreeTime", b =>
                {
                    b.HasOne("FixLife.WebApiDomain.Plan.Plan", null)
                        .WithMany("FreeTime")
                        .HasForeignKey("PlanId");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.Plan", b =>
                {
                    b.HasOne("FixLife.WebApiDomain.Plan.LearnTime", "LearnTime")
                        .WithMany()
                        .HasForeignKey("LearnTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FixLife.WebApiDomain.Plan.WeeklyWork", "WeeklyWork")
                        .WithMany()
                        .HasForeignKey("WeeklyWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LearnTime");

                    b.Navigation("WeeklyWork");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.UserPlan", b =>
                {
                    b.HasOne("FixLife.WebApiDomain.Plan.Plan", "Plans")
                        .WithMany()
                        .HasForeignKey("PlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FixLife.WebApiDomain.User.ClientUser", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plans");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("FixLife.WebApiDomain.Plan.Plan", b =>
                {
                    b.Navigation("FreeTime");
                });
#pragma warning restore 612, 618
        }
    }
}
