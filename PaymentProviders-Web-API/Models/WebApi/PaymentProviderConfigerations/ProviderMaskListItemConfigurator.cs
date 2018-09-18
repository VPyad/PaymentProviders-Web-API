using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations
{
    public class ProviderMaskListItemConfigurator : IEntityTypeConfiguration<ProviderMaskListItem>
    {
        public void Configure(EntityTypeBuilder<ProviderMaskListItem> builder)
        {
            builder.ToTable("ProviderMaskListItem");

            builder.HasOne(x => x.ProviderField)
                .WithMany(x => x.MaskListItem)
                .HasForeignKey(x=>x.ProviderFieldId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
