using API.Endpoints.AccountStatement.GetAccountStatement;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints.AccountStatement.GetAccontStatement;

public class GetAccountQuery
{
    private FinancesDbContext _dbContext;

    public GetAccountQuery(FinancesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAccountResponse> Query(int accountId, GetAccountQueryParameters parameters)
    {
        var account = await _dbContext.BankingAccounts.FirstAsync(x => x.Id == accountId);
        var query = _dbContext.BankingActions.Where(x => x.BankingAccount.Id == accountId);

        query = parameters.ApplyFilter(query);

        var actions = await query.ToListAsync();
        
        var payments = actions.Where(x =>
            x.ActionType == ActionType.Card || x.ActionType == ActionType.Outcome ||
            x.ActionType == ActionType.Withdrawal || x.ActionType == ActionType.BLIK).Sum(x => x.Value);

        var gains = actions.Where(x => x.ActionType == ActionType.Income).Sum(x => x.Value);

        return new GetAccountResponse
        {
            Account = account.AccountNumber,
            Id = account.Id,
            Label = account.Label,
            From = actions.Min(x => x.AccountingDate).Date,
            To = actions.Max(x => x.AccountingDate).Date,
            TransactionsCount = actions.Count,
            UnknownsCount = actions.Count(x => x.ActionType == ActionType.Unknown),
            Payments = payments.ToString("F2"),
            Gains = gains.ToString("F2")
        };
    }
}