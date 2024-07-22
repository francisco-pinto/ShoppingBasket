﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ShoppingBasketDbContext))]
    [Migration("20240720141229_AddStatusToBasket")]
    partial class AddStatusToBasket
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Domain.Basket.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PurchaseStatus")
                        .HasColumnType("int");

                    b.Property<int>("TotalPriceInCents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Domain.Domain.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DiscountType")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Discounts");

                    b.HasDiscriminator<int>("DiscountType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Domain.DiscountTypes.QuantityDiscount+ItemDiscount", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "Percentage");

                    b.HasIndex("DiscountId");

                    b.ToTable("ItemDiscount");
                });

            modelBuilder.Entity("Domain.Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PriceInCents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("DiscountId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Domain.Domain.DiscountTypes.PercentageDiscount", b =>
                {
                    b.HasBaseType("Domain.Domain.Discount");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Domain.Domain.DiscountTypes.QuantityDiscount", b =>
                {
                    b.HasBaseType("Domain.Domain.Discount");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Domain.Domain.DiscountTypes.QuantityDiscount+ItemDiscount", b =>
                {
                    b.HasOne("Domain.Domain.DiscountTypes.QuantityDiscount", null)
                        .WithMany("AssociatedItemDiscounts")
                        .HasForeignKey("DiscountId");

                    b.HasOne("Domain.Domain.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Domain.Item", b =>
                {
                    b.HasOne("Domain.Domain.Basket.Basket", null)
                        .WithMany("Items")
                        .HasForeignKey("BasketId");

                    b.HasOne("Domain.Domain.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Domain.Domain.Basket.Basket", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Domain.DiscountTypes.QuantityDiscount", b =>
                {
                    b.Navigation("AssociatedItemDiscounts");
                });
#pragma warning restore 612, 618
        }
    }
}
