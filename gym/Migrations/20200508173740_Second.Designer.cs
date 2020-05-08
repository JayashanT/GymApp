﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gym.Entity;

namespace gym.Migrations
{
    [DbContext(typeof(GymDbContext))]
    [Migration("20200508173740_Second")]
    partial class Second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("gym.Entity.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BreakTime");

                    b.Property<int>("ExerciseId");

                    b.Property<short>("NoOfSet");

                    b.Property<short>("Repition");

                    b.Property<int>("SheduleId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("SheduleId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("gym.Entity.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("gym.Entity.Gym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AllowedMembers");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime>("JoinedDate");

                    b.Property<string>("Logo");

                    b.Property<short>("MembershipDuration");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Gym");
                });

            modelBuilder.Entity("gym.Entity.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExerciseId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("gym.Entity.MembershipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DurationInMonths");

                    b.Property<int>("Fee");

                    b.Property<int>("GymId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("MembershipType");
                });

            modelBuilder.Entity("gym.Entity.Shedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("MemberId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Shedule");
                });

            modelBuilder.Entity("gym.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("GymId");

                    b.Property<string>("MobileNumber");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Role");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("gym.Entity.Admin", b =>
                {
                    b.HasBaseType("gym.Entity.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("gym.Entity.Member", b =>
                {
                    b.HasBaseType("gym.Entity.User");

                    b.Property<DateTime>("JoinedDate");

                    b.Property<string>("MembershipState");

                    b.Property<int>("MembershipTypeID");

                    b.Property<int?>("MembershipTypeId");

                    b.Property<string>("Name");

                    b.HasIndex("MembershipTypeId");

                    b.HasDiscriminator().HasValue("Member");
                });

            modelBuilder.Entity("gym.Entity.Event", b =>
                {
                    b.HasOne("gym.Entity.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gym.Entity.Shedule", "Shedule")
                        .WithMany("Events")
                        .HasForeignKey("SheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gym.Entity.Image", b =>
                {
                    b.HasOne("gym.Entity.Exercise", "Exercise")
                        .WithMany("Images")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gym.Entity.MembershipType", b =>
                {
                    b.HasOne("gym.Entity.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gym.Entity.Shedule", b =>
                {
                    b.HasOne("gym.Entity.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gym.Entity.User", b =>
                {
                    b.HasOne("gym.Entity.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gym.Entity.Member", b =>
                {
                    b.HasOne("gym.Entity.MembershipType", "MembershipType")
                        .WithMany()
                        .HasForeignKey("MembershipTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
