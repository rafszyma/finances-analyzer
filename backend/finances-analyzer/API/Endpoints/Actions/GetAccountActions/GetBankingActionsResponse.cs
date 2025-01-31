using System.Text.Json.Serialization;
using Database.Entities;

namespace API.Endpoints.Actions.GetAccountActions;

public class GetBankingActionsResponse
{
    public GetBankingActionsResponse(List<BankingAction> bankingActions)
    {
        Sum = bankingActions.Sum(x => x.Value);
        Count = bankingActions.Count;
        StartDate = bankingActions.Min(x => x.AccountingDate);
        EndDate = bankingActions.Max(x => x.AccountingDate);
        Actions = bankingActions.Select(x => new BankingActionDetails(x));
    }
    
    public double Sum { get; }
    
    public int Count { get; }
    
    public DateTime StartDate { get; } 
    
    public DateTime EndDate { get; }
    
    public IEnumerable<BankingActionDetails> Actions { get; set; }
}

public class BankingActionDetails
{
    public BankingActionDetails(BankingAction action)
    {
        Id = action.Id;
        Value = action.Value;
        Date = action.AccountingDate;
        ActionType = action.ActionType;
        Recipient = action.Recipient;
        Payload = action.Payload;
    }
    
    public int Id { get; set; }
    
    public double Value { get; set; }
    
    public DateTime Date { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActionType ActionType { get; set; }
    
    public string Recipient { get; set; }
    
    public string Payload { get; set; }
}