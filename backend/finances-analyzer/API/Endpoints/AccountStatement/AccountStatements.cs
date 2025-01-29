using API.Endpoints.AccountStatement.GetAccontStatement;
using API.Endpoints.AccountStatement.PutAccountStatements;

namespace API.Endpoints.AccountStatement;

public static class AccountStatements
{
    public static WebApplication MapAccountStatementEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("accounts");

        group.MapPutAccountStatementsEndpoints()
            .MapGetAccountEndpoints();
        return app;
    }

    public static IServiceCollection AddAccountStatements(this IServiceCollection services)
    {
        services.AddPutAccountStatements()
            .AddGetAccount();
        
        return services;
    }
}