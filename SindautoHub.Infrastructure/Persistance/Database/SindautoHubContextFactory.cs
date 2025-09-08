using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SindautoHub.Infrastructure.Persistance.Database;

public class SindautoHubContextFactory : IDesignTimeDbContextFactory<SindautoHubContext>
{
    public SindautoHubContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../SindautoHub.Api");

        Console.WriteLine("==> BasePath: " + basePath);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var conn = configuration.GetConnectionString("DefaultConnection");

        Console.WriteLine("==> Connection string lida: " + conn); 

        if (string.IsNullOrEmpty(conn))
        {
            throw new Exception("A connection string 'DefaultConnection' está vazia ou não foi encontrada.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<SindautoHubContext>();
        optionsBuilder.UseNpgsql(conn);

        return new SindautoHubContext(optionsBuilder.Options);
    }
}
