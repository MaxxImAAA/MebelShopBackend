﻿// <auto-generated />
using System;
using BackMebel.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackMebel.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231016153812_init2")]
    partial class init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackMebel.Domain.Models.CartModels.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.CartModels.CartProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.OrderModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountProduct")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.OrderModels.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.ProductModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAuthId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAuthId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.UserModels.UserAuth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAuths");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.CartModels.Cart", b =>
                {
                    b.HasOne("BackMebel.Domain.Models.UserModels.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("BackMebel.Domain.Models.CartModels.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.CartModels.CartProduct", b =>
                {
                    b.HasOne("BackMebel.Domain.Models.CartModels.Cart", "Cart")
                        .WithMany("CartProducts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackMebel.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("CartProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.OrderModels.Order", b =>
                {
                    b.HasOne("BackMebel.Domain.Models.UserModels.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.OrderModels.OrderProduct", b =>
                {
                    b.HasOne("BackMebel.Domain.Models.OrderModels.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackMebel.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.UserModels.User", b =>
                {
                    b.HasOne("BackMebel.Domain.Models.UserModels.UserAuth", "UserAuth")
                        .WithOne("User")
                        .HasForeignKey("BackMebel.Domain.Models.UserModels.User", "UserAuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAuth");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.CartModels.Cart", b =>
                {
                    b.Navigation("CartProducts");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.OrderModels.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.ProductModels.Product", b =>
                {
                    b.Navigation("CartProducts");

                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.UserModels.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BackMebel.Domain.Models.UserModels.UserAuth", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
