namespace API.Endpoints.AccountStatement.AddAccountStatements;

public static class PutAccountStatements
{
    public static RouteGroupBuilder MapPutAccountStatementsEndpoints(this RouteGroupBuilder group)
    {
        group.MapPut("", (IFormFile[] files, PutAccountStatementCommand command) =>
        {
            command.Execute(files);
            
            return Task.CompletedTask;
        });

        return group;
    }

    public static IServiceCollection AddPutAccountStatements(this IServiceCollection services)
    {
        services.AddScoped<PutAccountStatementCommand>()
            .AddScoped<PdfParser>();
        
        return services;
    }
}