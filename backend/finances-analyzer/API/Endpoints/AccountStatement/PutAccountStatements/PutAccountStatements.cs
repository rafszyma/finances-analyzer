namespace API.Endpoints.AccountStatement.PutAccountStatements;

public static class PutAccountStatements
{
    public static RouteGroupBuilder MapPutAccountStatementsEndpoints(this RouteGroupBuilder group)
    {
        group.MapPut("statements", async (IFormFileCollection files, PutAccountStatementCommand command) =>
        {
            var added = await command.Execute(files);

            return Results.Ok(added);
        })
        .DisableAntiforgery()
        .Produces<PutAccountStatementsResponse>();

        return group;
    }

    public static IServiceCollection AddPutAccountStatements(this IServiceCollection services)
    {
        services.AddScoped<PutAccountStatementCommand>()
            .AddScoped<PdfParser>();
        
        return services;
    }
}