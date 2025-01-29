using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[Table("BankingActionGroups")]
public class BankingActionGroup
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Label { get; set; }
    
    public string Color { get; set; }
    
    public BankingAction[] BankingActions { get; set; }
}