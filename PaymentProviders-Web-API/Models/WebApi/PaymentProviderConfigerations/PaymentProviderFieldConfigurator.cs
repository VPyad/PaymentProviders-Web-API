using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations
{
    public class PaymentProviderFieldConfigurator : IEntityTypeConfiguration<ProviderField>
    {
        public void Configure(EntityTypeBuilder<ProviderField> builder)
        {
            builder.ToTable("ProviderField");

            /*builder.HasOne(x => x.PaymentProvider)
                .WithMany(x => x.Fields)
                .OnDelete(DeleteBehavior.Cascade);*/

            builder.HasMany(x => x.MaskListItem)
                .WithOne(x => x.ProviderField)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
