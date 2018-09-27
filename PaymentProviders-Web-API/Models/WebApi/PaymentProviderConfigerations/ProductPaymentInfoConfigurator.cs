using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations
{
    public class ProductPaymentInfoConfigurator : IEntityTypeConfiguration<ProductPaymentInfo>
    {
        public void Configure(EntityTypeBuilder<ProductPaymentInfo> builder)
        {
            builder.ToTable("ProductPaymentInfo");

            builder.HasOne(x => x.PaymentInfo)
                .WithMany(x => x.ProductsPaymentInfo)
                .HasForeignKey(x => x.PaymentInfoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Commission)
                .WithOne(x => x.ProductPaymentInfo)
                .HasForeignKey<Commission>(x => x.ProductPaymentInfoRef)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
