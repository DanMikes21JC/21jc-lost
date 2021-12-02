using Lost.Common;
using Lost.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Lost.DataAccess.Entities
{
    internal class CommonDal
    {
        internal static LostDbContext CreateDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
            .Build();

            var builder = new DbContextOptionsBuilder<LostDbContext>();

            var connectionString = configuration.GetConnectionString(Constants.LostConnectionStringName);
            builder.UseNpgsql(connectionString);

            return new LostDbContext(builder.Options);
        }
    }
}
