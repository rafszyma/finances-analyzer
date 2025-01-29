using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[Table("BankingAccounts")]
public class BankingAccount
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string AccountNumber { get; set; }
    
    public string Label { get; set; }
    
    public IList<BankingAction> BankingActions { get; set; } = new List<BankingAction>();
}