using System.Text.RegularExpressions;
using Database;
using Database.Entities;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace API.Endpoints.AccountStatement.PutAccountStatements;

public class PdfParser
{
    private readonly ILogger<PdfParser> _logger;

    private readonly FinancesDbContext _financesDbContext;

    public PdfParser(ILogger<PdfParser> logger, FinancesDbContext financesDbContext)
    {
        _logger = logger;
        _financesDbContext = financesDbContext;
    }

    public IList<BankingAction> Parse(IFormFile file)
    {
        var result = new List<BankingAction>();

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            using (var pdf = PdfDocument.Open(reader.BaseStream))
            {
                var formatted = new List<string>();
                foreach (var page in pdf.GetPages())
                {
                    var text = ContentOrderTextExtractor.GetText(page);
                    formatted.AddRange(text.Split(Environment.NewLine));
                }

                var accountsIndexes = Enumerable.Range(0, formatted.Count)
                    .Where(i => formatted[i].Contains("NRB", StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

                for (int i = 0; i < accountsIndexes.Count; i++)
                {
                    var account = GetBankingAccount(formatted[accountsIndexes[i]]);

                    if (i + 1 < accountsIndexes.Count)
                    {
                        result.AddRange(GetForAccount(
                            formatted.GetRange(accountsIndexes[i], accountsIndexes[i + 1] - accountsIndexes[i]),
                            account));
                    }
                    else
                    {
                        result.AddRange(GetForAccount(
                            formatted.GetRange(accountsIndexes[i], formatted.Count - accountsIndexes[i]), account));
                    }
                }
            }
        }

        return result;
    }

    private BankingAccount GetBankingAccount(string accountString)
    {
        var index = accountString.IndexOf("NRB: ", StringComparison.InvariantCultureIgnoreCase);
        var accountNumber = accountString.Substring(index + 5, 32);

        var accountEntity = _financesDbContext.BankingAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

        if (accountEntity != null)
        {
            return accountEntity;
        }

        return new BankingAccount
        {
            AccountNumber = accountNumber,
            Label = accountNumber
        };
    }


    private IList<BankingAction> GetForAccount(List<string> lines, BankingAccount account)
    {
        var result = new List<BankingAction>();

        var headerStart = lines.FindIndex(0,
            line => line.Contains("OPIS TRANSAKCJI", StringComparison.InvariantCultureIgnoreCase));
        lines.RemoveRange(0, headerStart);
        lines.RemoveAll(x => x.Contains("OPIS TRANSAKCJI", StringComparison.InvariantCultureIgnoreCase));
        var records = GetActionRecords(lines);
        foreach (var record in records)
        {
            var action = BankingAction.Create(record, _logger, account);
            result.Add(action);
        }

        return result;
    }

    static List<List<string>> GetActionRecords(IEnumerable<string> lines)
    {
        var result = new List<List<string>>();
        var currentSubarray = new List<string>();
        var datePattern = new Regex(@"^\d{4}-\d{2}-\d{2}");

        foreach (var line in lines)
        {
            if (datePattern.IsMatch(line))
            {
                if (currentSubarray.Count > 0)
                {
                    result.Add([..currentSubarray]);
                    currentSubarray.Clear();
                }
            }

            currentSubarray.Add(line);
        }

        if (currentSubarray.Count > 0)
            result.Add(currentSubarray);

        return result;
    }
}