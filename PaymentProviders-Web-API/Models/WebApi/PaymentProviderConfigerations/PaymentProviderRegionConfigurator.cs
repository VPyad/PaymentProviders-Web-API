using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations
{
    public class PaymentProviderRegionConfigurator : IEntityTypeConfiguration<PaymentProviderRegion>
    {
        public void Configure(EntityTypeBuilder<PaymentProviderRegion> builder)
        {
            builder.ToTable("PaymentProviderRegion");

            builder.HasKey(x => new { x.PaymentProviderId, x.PaymentRegionId });

            builder.HasOne(x => x.PaymentRegion)
                .WithMany(x => x.Providers)
                .HasForeignKey(x => x.PaymentRegionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PaymentProvider)
                .WithMany(x => x.Regions)
                .HasForeignKey(x => x.PaymentProviderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
