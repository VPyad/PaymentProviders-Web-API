using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations
{
    public class PaymentInfoConfigurator : IEntityTypeConfiguration<PaymentInfo>
    {
        public void Configure(EntityTypeBuilder<PaymentInfo> builder)
        {
            builder.ToTable("PaymentInfo");

            /*builder.HasOne(x => x.PaymentProvider)
                .WithOne(x => x.PaymentInfo)
                .OnDelete(DeleteBehavior.Cascade);*/

            builder.HasMany(x => x.ProductsPaymentInfo)
                .WithOne(x => x.PaymentInfo)
                //.HasForeignKey(x => x.PaymentInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
