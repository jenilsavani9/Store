﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Entity.Data;

#nullable disable

namespace Store.Entity.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20230608043530_feature table user id")]
    partial class featuretableuserid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Store.Entity.Models.City", b =>
                {
                    b.Property<long>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CityId"));

                    b.Property<string>("CityName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<long>("StateId")
                        .HasColumnType("bigint");

                    b.HasKey("CityId")
                        .HasName("PK__City__F2D21B76707F60A7");

                    b.HasIndex("CountryId");

                    b.HasIndex("StateId");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("Store.Entity.Models.Country", b =>
                {
                    b.Property<long>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CountryId"));

                    b.Property<string>("CountryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CountryId")
                        .HasName("PK__Country__10D1609FC8DC8B72");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("Store.Entity.Models.Features", b =>
                {
                    b.Property<long>("FeaturesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("FeaturesId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeaturesDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeaturesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeaturesId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Store.Entity.Models.MailToken", b =>
                {
                    b.Property<long>("MailTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MailTokenId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Token")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("MailTokenId")
                        .HasName("PK__MailToke__2B0AEB8A38E3D339");

                    b.HasIndex("UserId");

                    b.ToTable("MailToken", (string)null);
                });

            modelBuilder.Entity("Store.Entity.Models.Roles", b =>
                {
                    b.Property<int>("RolesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolesId"));

                    b.Property<string>("RolesName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RolesId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Store.Entity.Models.State", b =>
                {
                    b.Property<long>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("StateId"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<string>("StateName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StateId")
                        .HasName("PK__State__C3BA3B3A75A0B61C");

                    b.HasIndex("CountryId");

                    b.ToTable("State", (string)null);
                });

            modelBuilder.Entity("Store.Entity.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("UserId")
                        .HasName("PK__User__1788CC4C18BD4CB4");

                    b.HasIndex(new[] { "Email" }, "UQ__User__A9D10534409B217B")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Store.Entity.Models.UserStore", b =>
                {
                    b.Property<long>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("StoreId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("LocationLink")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("PostalCode")
                        .HasColumnType("int");

                    b.Property<long>("StateId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StoreName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("StoreId")
                        .HasName("PK__UserStor__3B82F101E470CF37");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("StateId");

                    b.HasIndex("UserId");

                    b.ToTable("UserStore", (string)null);
                });

            modelBuilder.Entity("Store.Entity.Models.City", b =>
                {
                    b.HasOne("Store.Entity.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK__City__CountryId__4D94879B");

                    b.HasOne("Store.Entity.Models.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .IsRequired()
                        .HasConstraintName("FK__City__StateId__4E88ABD4");

                    b.Navigation("Country");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Store.Entity.Models.MailToken", b =>
                {
                    b.HasOne("Store.Entity.Models.User", "User")
                        .WithMany("MailTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__MailToken__UserI__3D5E1FD2");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Store.Entity.Models.State", b =>
                {
                    b.HasOne("Store.Entity.Models.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK__State__CountryId__47DBAE45");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Store.Entity.Models.UserStore", b =>
                {
                    b.HasOne("Store.Entity.Models.City", "City")
                        .WithMany("UserStores")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK__UserStore__CityI__6A30C649");

                    b.HasOne("Store.Entity.Models.Country", "Country")
                        .WithMany("UserStores")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK__UserStore__Count__68487DD7");

                    b.HasOne("Store.Entity.Models.State", "State")
                        .WithMany("UserStores")
                        .HasForeignKey("StateId")
                        .IsRequired()
                        .HasConstraintName("FK__UserStore__State__693CA210");

                    b.HasOne("Store.Entity.Models.User", "User")
                        .WithMany("UserStores")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__UserStore__UserI__6754599E");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Store.Entity.Models.City", b =>
                {
                    b.Navigation("UserStores");
                });

            modelBuilder.Entity("Store.Entity.Models.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("States");

                    b.Navigation("UserStores");
                });

            modelBuilder.Entity("Store.Entity.Models.State", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("UserStores");
                });

            modelBuilder.Entity("Store.Entity.Models.User", b =>
                {
                    b.Navigation("MailTokens");

                    b.Navigation("UserStores");
                });
#pragma warning restore 612, 618
        }
    }
}
