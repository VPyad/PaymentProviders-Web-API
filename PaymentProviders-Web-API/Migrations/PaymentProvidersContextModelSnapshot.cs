﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentProviders_Web_API.DbContexts;

namespace PaymentProvidersWebAPI.Migrations
{
    [DbContext(typeof(PaymentProvidersContext))]
    partial class PaymentProvidersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.Commission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CommissionValue");

                    b.Property<bool>("IsAbsolute");

                    b.Property<double>("MaxSum");

                    b.Property<double>("MinSum");

                    b.HasKey("Id");

                    b.ToTable("Commissions");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryCode");

                    b.Property<string>("NameRu");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("PaymentInfos");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentProvider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatalogCode");

                    b.Property<long?>("CategoryId");

                    b.Property<bool>("Check");

                    b.Property<string>("ChequeName");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("IsSupportRequestRSTEP");

                    b.Property<bool>("Mrlist");

                    b.Property<bool>("MultiCheck");

                    b.Property<string>("NameRu");

                    b.Property<bool>("NoSavePt");

                    b.Property<long>("Order");

                    b.Property<long?>("PaymentInfoId");

                    b.Property<string>("ProviderCode");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PaymentInfoId");

                    b.ToTable("PaymentProviders");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentRegion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code");

                    b.Property<string>("Name");

                    b.Property<long?>("PaymentProviderId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentProviderId");

                    b.ToTable("PaymentRegions");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProductPaymentInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CommissionId");

                    b.Property<double>("MaxSum");

                    b.Property<double>("MinSum");

                    b.Property<long?>("PaymentInfoId");

                    b.Property<int>("ProductType");

                    b.HasKey("Id");

                    b.HasIndex("CommissionId");

                    b.HasIndex("PaymentInfoId");

                    b.ToTable("ProductPaymentInfos");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProviderField", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("Direction");

                    b.Property<bool>("DontShow");

                    b.Property<bool>("DontTicket");

                    b.Property<int>("InterfaceType");

                    b.Property<string>("Mask");

                    b.Property<int?>("MaxLength");

                    b.Property<int?>("MinLength");

                    b.Property<string>("Name");

                    b.Property<long?>("PaymentProviderId");

                    b.Property<string>("RegExp");

                    b.Property<bool>("Required");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PaymentProviderId");

                    b.ToTable("ProviderFields");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProviderMaskListItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc");

                    b.Property<string>("Key");

                    b.Property<long?>("ProviderFieldId");

                    b.HasKey("Id");

                    b.HasIndex("ProviderFieldId");

                    b.ToTable("ProviderMaskListItems");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentProvider", b =>
                {
                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentInfo", "PaymentInfo")
                        .WithMany()
                        .HasForeignKey("PaymentInfoId");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentRegion", b =>
                {
                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentProvider")
                        .WithMany("Regions")
                        .HasForeignKey("PaymentProviderId");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProductPaymentInfo", b =>
                {
                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.Commission", "Commission")
                        .WithMany()
                        .HasForeignKey("CommissionId");

                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentInfo")
                        .WithMany("ProductsPaymentInfo")
                        .HasForeignKey("PaymentInfoId");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProviderField", b =>
                {
                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.PaymentProvider")
                        .WithMany("Fields")
                        .HasForeignKey("PaymentProviderId");
                });

            modelBuilder.Entity("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProviderMaskListItem", b =>
                {
                    b.HasOne("PaymentProviders_Web_API.Models.WebApi.PaymentsProviders.ProviderField")
                        .WithMany("MaskListItem")
                        .HasForeignKey("ProviderFieldId");
                });
#pragma warning restore 612, 618
        }
    }
}
