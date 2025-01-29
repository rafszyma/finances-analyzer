using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[Table("BankingActions")]
public class BankingAction
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTimeOffset AccountingDate { get; set; }
    
    public double Value { get; set; }
    
    public double Balance { get; set; }
    
    public ActionType ActionType { get; set; }
    
    public string Recipient { get; set; }
    
    public string Payload { get; set; }
    
    public BankingAccount BankingAccount { get; set; }
    
    public BankingActionGroup? BankingActionGroup { get; set; }
}

public enum ActionType
{
    BLIK = 1,
    Income,
    Outcome,
    Withdrawal,
    Card
}