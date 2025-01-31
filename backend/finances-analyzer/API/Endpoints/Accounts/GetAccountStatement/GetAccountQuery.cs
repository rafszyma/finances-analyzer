using Database;
using Database.Entities;
using Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints.AccountStatement.GetAccontStatement;

public class GetAccountQuery
{
    private FinancesDbContext _dbContext;

    public GetAccountQuery(FinancesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAccountResponse> Query(int accountId, ActionFilter filter)
    {
        var account = await _dbContext.BankingAccounts.FirstAsync(x => x.Id == accountId);
        var query = _dbContext.BankingActions.ApplyActionFilter(filter).Where(x => x.BankingAccount.Id == accountId);

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