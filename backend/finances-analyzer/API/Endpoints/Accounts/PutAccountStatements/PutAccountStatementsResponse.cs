namespace API.Endpoints.Accounts.PutAccountStatements;

public record PutAccountStatementsResponse(List<FileImportResults> Results);

public record FileImportResults(DateTime From, DateTime To, int Results, IEnumerable<string> Accounts);