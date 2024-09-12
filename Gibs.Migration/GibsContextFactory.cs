using System.Diagnostics;
using Gibs.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gibs.Migrations
{
    public class GibsContextFactory : IDesignTimeDbContextFactory<GibsContext>
    {
        public static GibsContext CreateDbContext()
        {
            var factory = new GibsContextFactory();
            return factory.CreateDbContext([]);
        }

        //needed by Add-Migrations at design time
        public GibsContext CreateDbContext(string[] args)
        {
            var conn = "Data Source=tcp:mssql-124479-0.cloudclusters.net,10018;Initial Catalog=gibs-core-db;User ID=idevworks;Password=1N$pirion;TrustServerCertificate=true";
            
            var optionsBuilder = new DbContextOptionsBuilder<GibsContext>();
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.UseSqlServer(conn, b => b.MigrationsAssembly("Gibs7.Migrations"));

            return new GibsContext(optionsBuilder.Options);
        }
    }
}