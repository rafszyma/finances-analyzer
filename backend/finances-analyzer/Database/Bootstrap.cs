using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Database;

public static class Bootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("npgsql");
        var options = new DbOptions();
        section.Bind(options);
        
        var csBuilder = new NpgsqlConnectionStringBuilder
        {
            Database = options.Database,
            Host = options.Host,
            Port = options.Port,
            Username = options.Username,
            Password = options.Password
        };

        services.AddDbContext<FinancesDbContext>(builder =>
        {
            builder.UseNpgsql(csBuilder.ConnectionString);
        });
        
        return services;
    }

    private class DbOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}