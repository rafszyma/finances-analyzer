using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Database;

public class FinancesDbContext : DbContext
{
    public FinancesDbContext()
    {
        
    }
    
    public FinancesDbContext(DbContextOptions<FinancesDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            var csBuilder = new NpgsqlConnectionStringBuilder
            {
                Database = "analyzerdb",
                Host = "localhost",
                Port = 6432,
                Username = "analyzeruser",
                Password = "analyzerpass"
            };

            optionsBuilder.UseNpgsql(csBuilder.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BankingAction>(entity =>
        {
            entity.HasOne(x => x.BankingAccount)
                .WithMany(x => x.BankingActions);
            entity.HasOne(x => x.BankingActionGroup)
                .WithMany(x => x.BankingActions);
        });
    }
    
    DbSet<BankingAction> BankingActions { get; set; }
    
    DbSet<BankingAccount> BankingAccounts { get; set; }
    
    DbSet<BankingActionGroup> BankingActionGroups { get; set; }
}