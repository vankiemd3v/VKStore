using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.Data.EF
{
    public class VKStoreDbContextFactory : IDesignTimeDbContextFactory<VKStoreDbContext>
    {
        public VKStoreDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("VKStoreDb");

            var optionsBuilder = new DbContextOptionsBuilder<VKStoreDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new VKStoreDbContext(optionsBuilder.Options);
        }
    }
}
