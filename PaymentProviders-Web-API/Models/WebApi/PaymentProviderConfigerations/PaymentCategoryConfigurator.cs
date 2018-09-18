using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentProviderConfigerations
{
    public class PaymentCategoryConfigurator : IEntityTypeConfiguration<PaymentCategory>
    {
        public void Configure(EntityTypeBuilder<PaymentCategory> builder)
        {
            builder.ToTable("PaymentCategory");

            builder.HasMany(x => x.Providers)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
