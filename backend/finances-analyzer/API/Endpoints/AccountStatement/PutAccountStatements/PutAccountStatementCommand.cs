namespace API.Endpoints.AccountStatement.AddAccountStatements;

public class PutAccountStatementCommand
{
    private readonly PdfParser _parser;
    public PutAccountStatementCommand(PdfParser parser)
    {
        _parser = parser;
    }
    
    public async Task Execute(IFormFile[] files)
    {
        
    }
}