﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS_System_API.Data;

#nullable disable

namespace POS_System_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240718150359_updatedPurchaseModels")]
    partial class updatedPurchaseModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("POS_System_API.Entities.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GrossTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalesTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.BillProduct", b =>
                {
                    b.Property<Guid>("BillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BillId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("BillProducts");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackSize")
                        .HasColumnType("int");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RetailPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GrossTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OtherCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalesTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.PurchaseProduct", b =>
                {
                    b.Property<Guid>("PurchaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PurchaseId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("PurchaseProducts");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.BillProduct", b =>
                {
                    b.HasOne("POS_System_API.Entities.Models.Bill", "Bill")
                        .WithMany("BillProducts")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS_System_API.Entities.Models.Product", "Product")
                        .WithMany("BillProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.PurchaseProduct", b =>
                {
                    b.HasOne("POS_System_API.Entities.Models.Product", "Product")
                        .WithMany("PurchaseProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS_System_API.Entities.Models.Purchase", "Purchase")
                        .WithMany("PurchaseProducts")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Stock", b =>
                {
                    b.HasOne("POS_System_API.Entities.Models.Product", "Product")
                        .WithOne("Stock")
                        .HasForeignKey("POS_System_API.Entities.Models.Stock", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Bill", b =>
                {
                    b.Navigation("BillProducts");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Product", b =>
                {
                    b.Navigation("BillProducts");

                    b.Navigation("PurchaseProducts");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("POS_System_API.Entities.Models.Purchase", b =>
                {
                    b.Navigation("PurchaseProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
