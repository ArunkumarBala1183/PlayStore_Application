﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Playstore.Migrations;

#nullable disable

namespace Playstore.Migrations.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Developer")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("App");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("AppFile")
                        .HasColumnType("BLOB");

                    b.Property<Guid>("AppId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppId")
                        .IsUnique();

                    b.ToTable("AppDatas");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppDownloads", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DownloadedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId");

                    b.ToTable("AppDownloads");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppImages", b =>
                {
                    b.Property<Guid>("AppImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AppInfoAppId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Image")
                        .HasColumnType("BLOB");

                    b.HasKey("AppImageId");

                    b.HasIndex("AppInfoAppId");

                    b.ToTable("AppImages");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppInfo", b =>
                {
                    b.Property<Guid>("AppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("BLOB");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublisherName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("AppId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("AppInfo");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AppReviews");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryName")
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleCode")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.UserCredentials", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserCredentials");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.Users", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppData", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.AppInfo", "AppInfo")
                        .WithOne("AppData")
                        .HasForeignKey("Playstore.Contracts.Data.Entities.AppData", "AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppInfo");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppDownloads", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.AppInfo", "AppInfo")
                        .WithMany("AppDownloads")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Playstore.Contracts.Data.Entities.Users", "Users")
                        .WithMany("AppDownloads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppInfo");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppImages", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.AppInfo", "AppInfo")
                        .WithMany("AppImages")
                        .HasForeignKey("AppInfoAppId");

                    b.Navigation("AppInfo");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppInfo", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.Category", "Category")
                        .WithMany("AppInfo")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Playstore.Contracts.Data.Entities.Users", "Users")
                        .WithMany("AppInfo")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppReview", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.AppInfo", "AppInfo")
                        .WithMany("AppReview")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Playstore.Contracts.Data.Entities.Users", "Users")
                        .WithOne("AppReview")
                        .HasForeignKey("Playstore.Contracts.Data.Entities.AppReview", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppInfo");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.RefreshToken", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.Users", "Users")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Playstore.Contracts.Data.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.UserCredentials", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.Users", "User")
                        .WithOne("userCredentials")
                        .HasForeignKey("Playstore.Contracts.Data.Entities.UserCredentials", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.UserRole", b =>
                {
                    b.HasOne("Playstore.Contracts.Data.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Playstore.Contracts.Data.Entities.Users", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.AppInfo", b =>
                {
                    b.Navigation("AppData");

                    b.Navigation("AppDownloads");

                    b.Navigation("AppImages");

                    b.Navigation("AppReview");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.Category", b =>
                {
                    b.Navigation("AppInfo");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Playstore.Contracts.Data.Entities.Users", b =>
                {
                    b.Navigation("AppDownloads");

                    b.Navigation("AppInfo");

                    b.Navigation("AppReview");

                    b.Navigation("RefreshToken");

                    b.Navigation("UserRoles");

                    b.Navigation("userCredentials");
                });
#pragma warning restore 612, 618
        }
    }
}
