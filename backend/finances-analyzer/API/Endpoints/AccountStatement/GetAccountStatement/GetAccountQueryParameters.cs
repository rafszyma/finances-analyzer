
using Database.Entities;

namespace API.Endpoints.AccountStatement.GetAccountStatement;


public class GetAccountQueryParameters
{
    public DateTime? From { get; set; }
    
    public DateTime? To { get; set; }
    
    public ActionType[]? Actions { get; set; }

    public IQueryable<BankingAction> ApplyFilter(IQueryable<BankingAction> query)
    {
        if (From.HasValue)
        {
            query = query.Where(x => x.AccountingDate >= From);
        }
        
        if (To.HasValue)
        {
            query = query.Where(x => x.AccountingDate <= To);
        }

        if (Actions is { Length: > 0 })
        {
            query = query.Where(x => Actions.Contains(x.ActionType));
        }

        return query;
    }
}