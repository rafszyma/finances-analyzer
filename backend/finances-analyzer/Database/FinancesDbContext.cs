using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class FinancesDbContext : DbContext
{
    public FinancesDbContext()
    {
        
    }
    
    public FinancesDbContext(DbContextOptions<FinancesDbContext> options) : base(options) { }

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