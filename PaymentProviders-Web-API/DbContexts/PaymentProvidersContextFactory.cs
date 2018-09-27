using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.DbContexts
{
    public class PaymentProvidersContextFactory : IDesignTimeDbContextFactory<PaymentProvidersContext>
    {
        public PaymentProvidersContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PaymentProvidersContext>();

            builder.UseSqlServer("Server=.;Database=PaymentProvidersDB;Trusted_Connection=True",
                optionBuilder => optionBuilder.MigrationsAssembly(typeof(PaymentProvidersContext).GetTypeInfo().Assembly.GetName().Name));

            return new PaymentProvidersContext(builder.Options);
        }
    }
}
