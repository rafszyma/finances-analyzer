using Database.Entities;

namespace Database.Extensions;

public class ActionFilter
{
    public DateTime? DateFrom { get; set; }
    
    public DateTime? DateTo { get; set; }
    
    public ActionType[]? Actions { get; set; }
    
    public double? ValueFrom { get; set; }
    
    public double? ValueTo { get; set; }
}

public static class ActionFilterExtension
{
    public static IQueryable<BankingAction> ApplyActionFilter(this IQueryable<BankingAction> query, ActionFilter filter)
    {
        if (filter.DateFrom.HasValue)
        {
            query = query.Where(x => x.AccountingDate >= filter.DateFrom);
        }
        
        if (filter.DateTo.HasValue)
        {
            query = query.Where(x => x.AccountingDate <= filter.DateTo);
        }

        if (filter.ValueFrom.HasValue)
        {
            query = query.Where(x => x.Value >= filter.ValueFrom);
        }

        if (filter.ValueTo.HasValue)
        {
            query = query.Where(x => x.Value <= filter.ValueTo);
        }

        if (filter.Actions is { Length: > 0 })
        {
            query = query.Where(x => filter.Actions.Contains(x.ActionType));
        }

        return query;
    }
}