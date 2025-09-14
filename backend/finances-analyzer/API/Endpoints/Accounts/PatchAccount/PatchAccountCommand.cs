using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints.Accounts.PatchAccount;

public class PatchAccountCommand
{
    private readonly FinancesDbContext _dbContext;

    public PatchAccountCommand(FinancesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Execute(PatchAccountRequest request)
    {
        var account = await _dbContext.BankingAccounts.FirstAsync(x => x.Id == request.Id);

        if (request.Label != null)
        {
            account.Label = request.Label;
        }

        if (request.Color != null)
        {
            account.Color = request.Color;
        }

        await _dbContext.SaveChangesAsync();
    }
}