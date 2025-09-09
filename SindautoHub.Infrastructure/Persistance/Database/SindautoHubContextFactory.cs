using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SindautoHub.Infrastructure.Persistance.Database;

public class SindautoHubContextFactory : IDesignTimeDbContextFactory<SindautoHubContext>
{
    public SindautoHubContext CreateDbContext(string[] args)
    {
        // 1) tenta pegar da env var (bom p/ CI/CD)
        var envConn = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        // 2) basePath aponta para o projeto da API (onde estão os appsettings)
        var infraPath = Directory.GetCurrentDirectory();
        var apiPath = Path.Combine(infraPath, "..", "SindautoHub.Api");

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.Exists(apiPath) ? apiPath : infraPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = envConn ?? config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

        var options = new DbContextOptionsBuilder<SindautoHubContext>()
            .UseNpgsql(connectionString, npgsql =>
            {
                // opcional: define a tabela do histórico de migrations
                npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "public");
            })
            .Options;

        return new SindautoHubContext(options);
    }
}
