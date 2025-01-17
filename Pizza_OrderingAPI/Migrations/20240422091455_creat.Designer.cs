﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizza_OrderingAPI.Data;

#nullable disable

namespace Pizza_OrderingAPI.Migrations
{
    [DbContext(typeof(ShopOnlineDbContext))]
    [Migration("20240422091455_creat")]
    partial class creat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("ExtraItemsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CartItemId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.City", b =>
                {
                    b.Property<string>("CityCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityCode");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            CityCode = "C1",
                            CityName = "City 1"
                        },
                        new
                        {
                            CityCode = "C2",
                            CityName = "City 2"
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ExtraItem", b =>
                {
                    b.Property<int>("ExtraItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExtraItemId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ExtraItemId");

                    b.ToTable("ExtraItems");

                    b.HasData(
                        new
                        {
                            ExtraItemId = 1,
                            Name = "Extra Cheese",
                            Price = 1.50m
                        },
                        new
                        {
                            ExtraItemId = 2,
                            Name = "Bacon",
                            Price = 2.00m
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ItemCategory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Category 1"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Category 2"
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DesiredDeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            CustomerId = 1,
                            DeliveryAddress = "123 Main St",
                            DesiredDeliveryTime = new DateTime(2024, 4, 23, 11, 14, 54, 635, DateTimeKind.Local).AddTicks(6749),
                            OrderDate = new DateTime(2024, 4, 22, 11, 14, 54, 635, DateTimeKind.Local).AddTicks(6626),
                            StatusId = 1,
                            TotalAmount = 29.97m
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.OrderConfirmation", b =>
                {
                    b.Property<int>("ConfirmationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfirmationId"), 1L, 1);

                    b.Property<DateTime>("ConfirmationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConfirmationMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("ConfirmationId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderConfirmations");

                    b.HasData(
                        new
                        {
                            ConfirmationId = 1,
                            ConfirmationDate = new DateTime(2024, 4, 22, 11, 14, 54, 635, DateTimeKind.Local).AddTicks(6809),
                            ConfirmationMessage = "Your order has been confirmed.",
                            OrderId = 1
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PizzaId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemId = 1,
                            OrderId = 1,
                            PizzaId = 1,
                            Quantity = 2,
                            Size = "Small",
                            Subtotal = 17.98m
                        },
                        new
                        {
                            OrderItemId = 2,
                            OrderId = 1,
                            PizzaId = 2,
                            Quantity = 1,
                            Size = "Medium",
                            Subtotal = 11.99m
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.OrderStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"), 1L, 1);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusName = "Pending"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusName = "Confirmed"
                        },
                        new
                        {
                            StatusId = 3,
                            StatusName = "Delivered"
                        },
                        new
                        {
                            StatusId = 4,
                            StatusName = "Cancelled"
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ProductMenu", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PizzaId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PizzaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PizzaNum")
                        .HasColumnType("int");

                    b.HasKey("PizzaId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductMenus");

                    b.HasData(
                        new
                        {
                            PizzaId = 1,
                            CategoryId = 1,
                            Description = "Tomato, cheese, basil",
                            PizzaName = "Margherita",
                            PizzaNum = 1
                        },
                        new
                        {
                            PizzaId = 2,
                            CategoryId = 2,
                            Description = "Pepperoni, cheese, tomato sauce",
                            PizzaName = "Pepperoni",
                            PizzaNum = 2
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ProductSize", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SizeId");

                    b.HasIndex("PizzaId");

                    b.ToTable("PizzaSizes");

                    b.HasData(
                        new
                        {
                            SizeId = 1,
                            Name = "Small",
                            PizzaId = 1,
                            Price = 8.99m
                        },
                        new
                        {
                            SizeId = 2,
                            Name = "Medium",
                            PizzaId = 1,
                            Price = 10.99m
                        },
                        new
                        {
                            SizeId = 3,
                            Name = "Large",
                            PizzaId = 1,
                            Price = 12.99m
                        },
                        new
                        {
                            SizeId = 4,
                            Name = "Small",
                            PizzaId = 2,
                            Price = 9.99m
                        },
                        new
                        {
                            SizeId = 5,
                            Name = "Medium",
                            PizzaId = 2,
                            Price = 11.99m
                        },
                        new
                        {
                            SizeId = 6,
                            Name = "Large",
                            PizzaId = 2,
                            Price = 13.99m
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.User", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CityCode");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "123 Main St",
                            CityCode = "C1",
                            Email = "john@example.com",
                            Name = "John Doe",
                            PhoneNumber = "123456789"
                        },
                        new
                        {
                            CustomerId = 2,
                            Address = "456 Elm St",
                            CityCode = "C2",
                            Email = "jane@example.com",
                            Name = "Jane Smith",
                            PhoneNumber = "987654321"
                        });
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.Order", b =>
                {
                    b.HasOne("Pizza_OrderingAPI.Entities.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizza_OrderingAPI.Entities.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.OrderConfirmation", b =>
                {
                    b.HasOne("Pizza_OrderingAPI.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.OrderItem", b =>
                {
                    b.HasOne("Pizza_OrderingAPI.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizza_OrderingAPI.Entities.ProductMenu", "Pizza")
                        .WithMany()
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ProductMenu", b =>
                {
                    b.HasOne("Pizza_OrderingAPI.Entities.ItemCategory", "ItemCategory")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemCategory");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ProductSize", b =>
                {
                    b.HasOne("Pizza_OrderingAPI.Entities.ProductMenu", "Pizza")
                        .WithMany("Sizes")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.User", b =>
                {
                    b.HasOne("Pizza_OrderingAPI.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Pizza_OrderingAPI.Entities.ProductMenu", b =>
                {
                    b.Navigation("Sizes");
                });
#pragma warning restore 612, 618
        }
    }
}
