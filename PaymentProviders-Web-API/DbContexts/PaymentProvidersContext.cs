using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.DbContexts
{
    public class PaymentProvidersContext : DbContext
    {
        public DbSet<Commission> Commissions { get; set; }

        public DbSet<PaymentCategory> Categories { get; set; }

        public DbSet<PaymentInfo> PaymentInfos { get; set; }

        public DbSet<PaymentRegion> PaymentRegions { get; set; }

        public DbSet<ProductPaymentInfo> ProductPaymentInfos { get; set; }

        public DbSet<ProviderField> ProviderFields { get; set; }

        public DbSet<ProviderMaskListItem> ProviderMaskListItems { get; set; }

        public DbSet<PaymentProvider> PaymentProviders { get; set; }

        public PaymentProvidersContext(DbContextOptions options) : base(options)
        { }

        public PaymentProvidersContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PaymentProvidersDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            var productPaymentInfoTypeEnumConverter = new EnumToStringConverter<ProductPaymentInfoType>();
            var fieldTypeEnumConverter = new EnumToStringConverter<FieldType>();
            var fieldInerfaceTypeEnumConverter = new EnumToStringConverter<FieldInerfaceType>();

            modelBuilder.Entity<ProviderField>().Property(x => x.Type).HasConversion(fieldTypeEnumConverter);
            modelBuilder.Entity<ProviderField>().Property(x => x.InterfaceType).HasConversion(fieldInerfaceTypeEnumConverter);
            modelBuilder.Entity<ProductPaymentInfo>().Property(x => x.ProductType).HasConversion(productPaymentInfoTypeEnumConverter);

            modelBuilder.ApplyConfiguration(new PaymentCategoryConfigurator());
            modelBuilder.ApplyConfiguration(new PaymentCommissionConfigurator());
            modelBuilder.ApplyConfiguration(new PaymentInfoConfigurator());
            modelBuilder.ApplyConfiguration(new PaymentProviderConfigurator());
            modelBuilder.ApplyConfiguration(new PaymentProviderFieldConfigurator());
            modelBuilder.ApplyConfiguration(new PaymentProviderRegionConfigurator());
            modelBuilder.ApplyConfiguration(new ProductPaymentInfoConfigurator());
            modelBuilder.ApplyConfiguration(new ProviderMaskListItemConfigurator());
        }
    }
}
