using API.Endpoints.AccountStatement.GetAccountActions;
using Database;
using Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints.Actions.GetAccountActions;

public class GetBankingActionsQuery
{
    private readonly FinancesDbContext _dbContext;

    public GetBankingActionsQuery(FinancesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBankingActionsResponse> Query(ActionFilter filter)
    {
        var actions = await _dbContext.BankingActions.ApplyActionFilter(filter).ToListAsync();

        var result = new GetBankingActionsResponse(actions);

        return result;
    }
}