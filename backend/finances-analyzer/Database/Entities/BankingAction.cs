using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Utils;
using StringComparison = System.StringComparison;

namespace Database.Entities;

[Table("BankingActions")]
public class BankingAction
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime AccountingDate { get; set; }
    
    public double Value { get; set; }
    
    public double Balance { get; set; }
    
    public ActionType ActionType { get; set; }
    
    public string Recipient { get; set; }
    
    public string Payload { get; set; }
    
    public BankingAccount BankingAccount { get; set; }
    
    public BankingActionGroup? BankingActionGroup { get; set; }

    public static BankingAction Create(List<string> lines, ILogger logger, BankingAccount bankingAccount)
    {
        var firstLine = lines[0];
        var values = firstLine.Split(' ');
        var accountingDate = DateTime.Parse(values.First());
        accountingDate = DateTime.SpecifyKind(accountingDate, DateTimeKind.Utc);

        var moneyValues = values.TakeLast(2).ToList();
        var balance = double.Parse(moneyValues.Last().Sanitize());
        var value = double.Parse(moneyValues.First().Sanitize());
        var recipient = lines.Skip(1).First();
        var payload = JsonSerializer.Serialize(lines);


        return new BankingAction
        {
            AccountingDate = accountingDate,
            Value = value,
            Balance = balance,
            ActionType = ActionTypeExtensions.FromStringArray(values, logger),
            Recipient = recipient,
            Payload = payload,
            BankingAccount = bankingAccount
        };
    }
}

public enum ActionType
{
    BLIK = 1,
    Income,
    Outcome,
    Withdrawal,
    Card,
    Unknown,
    Commission,
    InterestGain,
    Installment
}


public static class ActionTypeExtensions
{
    public static ActionType FromStringArray(string[] array, ILogger logger)
    {
        if (array.Any(x => x.Contains("WYPŁATA", StringComparison.InvariantCultureIgnoreCase)) &&
            array.Any(x => x.Contains("BANKOMATU", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.Withdrawal;
        }
        
        if (array.Any(x => x.Contains("BLIK", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.BLIK;
        }

        if (array.Any(x => x.Contains("PRZYCHODZĄCY", StringComparison.InvariantCultureIgnoreCase)) || array.Any(x => x.Contains("WPŁATA", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.Income;
        }

        if (array.Any(x => x.Contains("KARTĄ", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.Card;
        }
        
        if (array.Any(x => x.Contains("PROWIZJA", StringComparison.InvariantCultureIgnoreCase)) || 
            array.Any(x => x.Contains("OBCIĄŻENIE", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.Commission;
        }
        
        if (array.Any(x => x.Contains("UZNANIE", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.InterestGain;
        }
        
        if (array.Any(x => x.Contains("SPŁ.RATY-REGULARNA", StringComparison.InvariantCultureIgnoreCase)))
        {
            return ActionType.Installment;
        }
        
        if (array.Any(x => x.Contains("WYCHODZĄCY", StringComparison.InvariantCultureIgnoreCase) ||
                           x.Contains("STAŁE", StringComparison.InvariantCultureIgnoreCase)) || 
            (array.Any(x => x.Contains("NA", StringComparison.InvariantCultureIgnoreCase)) &&
             array.Any(x => x.Contains("TELEFONU", StringComparison.InvariantCultureIgnoreCase))))
        {
            return ActionType.Outcome;
        }

        logger.LogWarning("Object {Array} does not have valid action type", JsonSerializer.Serialize(array));
        return ActionType.Unknown;
    }
}