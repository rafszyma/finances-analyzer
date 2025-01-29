using Database;

namespace API.Endpoints.AccountStatement.PutAccountStatements;

public class PutAccountStatementCommand
{
    private readonly PdfParser _parser;
    private readonly FinancesDbContext _dbContext;

    public PutAccountStatementCommand(PdfParser parser, FinancesDbContext dbContext)
    {
        _parser = parser;
        _dbContext = dbContext;
    }

    public async Task<PutAccountStatementsResponse> Execute(IFormFileCollection files)
    {
        var results = new List<FileImportResults>();
        foreach (var file in files)
        {
            var entities = _parser.Parse(file);

            var bankingAccounts = entities.Select(x => x.BankingAccount).Distinct().ToArray();
            var bankingAccountIds = bankingAccounts.Select(x => x.Id);
            var from = entities.Min(x => x.AccountingDate);
            var to = entities.Max(x => x.AccountingDate);

            _dbContext.BankingActions.RemoveRange(_dbContext.BankingActions.Where(x =>
                bankingAccountIds.Contains(x.BankingAccount.Id) && x.AccountingDate >= from && x.AccountingDate <= to));
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            results.Add(new FileImportResults(from, to, entities.Count, bankingAccounts.Select(x => x.Label)));

        }

        return new PutAccountStatementsResponse(results);
    }
}