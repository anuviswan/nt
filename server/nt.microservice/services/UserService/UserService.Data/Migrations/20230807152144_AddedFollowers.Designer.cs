﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Data.Database;

#nullable disable

namespace UserService.Data.Migrations
{
    [DbContext(typeof(UserserviceDbContext))]
    [Migration("20230807152144_AddedFollowers")]
    partial class AddedFollowers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UserService.Domain.Entities.UserFollower", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("FolloweeId")
                        .HasColumnType("bigint");

                    b.Property<long>("FollowerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FolloweeId");

                    b.HasIndex("FollowerId");

                    b.ToTable("UserFollower");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FolloweeId = 1L,
                            FollowerId = 2L
                        },
                        new
                        {
                            Id = 2L,
                            FolloweeId = 1L,
                            FollowerId = 3L
                        },
                        new
                        {
                            Id = 3L,
                            FolloweeId = 2L,
                            FollowerId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            FolloweeId = 2L,
                            FollowerId = 3L
                        });
                });

            modelBuilder.Entity("UserService.Domain.Entities.UserMetaInformation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserMetaInformation");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Bio = "I am Jia anu",
                            DisplayName = "Jia Anu",
                            UserName = "jia.anu"
                        },
                        new
                        {
                            Id = 2L,
                            Bio = "I am Naina anu",
                            DisplayName = "Naina Anu",
                            UserName = "naina.anu"
                        },
                        new
                        {
                            Id = 3L,
                            Bio = "I am Sreena anu",
                            DisplayName = "Sreena Anu",
                            UserName = "sreena.anu"
                        },
                        new
                        {
                            Id = 4L,
                            Bio = "I am Admin",
                            DisplayName = "Admin",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("UserService.Domain.Entities.UserFollower", b =>
                {
                    b.HasOne("UserService.Domain.Entities.UserMetaInformation", "Followee")
                        .WithMany("Following")
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("UserService.Domain.Entities.UserMetaInformation", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Followee");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("UserService.Domain.Entities.UserMetaInformation", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Following");
                });
#pragma warning restore 612, 618
        }
    }
}
