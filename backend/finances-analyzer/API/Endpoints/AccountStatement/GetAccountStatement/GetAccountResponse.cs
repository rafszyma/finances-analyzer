namespace API.Endpoints.AccountStatement.GetAccontStatement;

public class GetAccountResponse
{
    public int Id { get; set; }
    
    public string Label { get; set; }
    
    public string Account { get; set; }
    
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
    
    public int TransactionsCount { get; set; }
    
    public int UnknownsCount { get; set; }
    
    public string Payments { get; set; }
    
    public string Gains { get; set; }
}