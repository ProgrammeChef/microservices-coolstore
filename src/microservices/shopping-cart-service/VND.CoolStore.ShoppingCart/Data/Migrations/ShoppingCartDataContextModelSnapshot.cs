﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VND.CoolStore.ShoppingCart.Data;

namespace VND.CoolStore.ShoppingCart.Data.Migrations
{
    [DbContext(typeof(ShoppingCartDataContext))]
    partial class ShoppingCartDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VND.CoolStore.ShoppingCart.Domain.Cart.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CartItemPromoSavings")
                        .HasColumnType("float");

                    b.Property<double>("CartItemTotal")
                        .HasColumnType("float");

                    b.Property<double>("CartTotal")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCheckout")
                        .HasColumnType("bit");

                    b.Property<double>("ShippingPromoSavings")
                        .HasColumnType("float");

                    b.Property<double>("ShippingTotal")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Carts","cart");
                });

            modelBuilder.Entity("VND.CoolStore.ShoppingCart.Domain.Cart.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrentCartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PromoSavings")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CurrentCartId");

                    b.ToTable("CartItems","cart");
                });

            modelBuilder.Entity("VND.CoolStore.ShoppingCart.Domain.Cart.ProductCatalogId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrentCartItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CurrentCartItemId")
                        .IsUnique();

                    b.ToTable("ProductCatalogIds","cart");
                });

            modelBuilder.Entity("VND.CoolStore.ShoppingCart.Domain.ProductCatalog.ProductCatalog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductCatalogs","catalog");

                    b.HasData(
                        new
                        {
                            Id = new Guid("df79f461-e985-4ebe-bf65-922bc85a6f8d"),
                            Created = new DateTime(2019, 9, 26, 17, 22, 59, 746, DateTimeKind.Utc).AddTicks(6695),
                            Desc = "quis nostrud exercitation ull",
                            ImagePath = "https://picsum.photos/1200/900?image=1",
                            Name = "tempor incididunt ut labore et do",
                            Price = 638.0,
                            ProductId = new Guid("05233341-185a-468a-b074-00ebd08559aa"),
                            Version = 0
                        },
                        new
                        {
                            Id = new Guid("be559748-37b4-488f-bb31-deae8109895b"),
                            Created = new DateTime(2019, 9, 26, 17, 22, 59, 746, DateTimeKind.Utc).AddTicks(9546),
                            Desc = "sin",
                            ImagePath = "https://picsum.photos/1200/900?image=1",
                            Name = "m",
                            Price = 671.0,
                            ProductId = new Guid("3cb275c5-aa53-40ff-bc6a-015327053af9"),
                            Version = 0
                        });
                });

            modelBuilder.Entity("VND.CoolStore.ShoppingCart.Domain.Cart.CartItem", b =>
                {
                    b.HasOne("VND.CoolStore.ShoppingCart.Domain.Cart.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CurrentCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VND.CoolStore.ShoppingCart.Domain.Cart.ProductCatalogId", b =>
                {
                    b.HasOne("VND.CoolStore.ShoppingCart.Domain.Cart.CartItem", "CartItem")
                        .WithOne("Product")
                        .HasForeignKey("VND.CoolStore.ShoppingCart.Domain.Cart.ProductCatalogId", "CurrentCartItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
