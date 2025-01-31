using API.Endpoints.AccountStatement.GetAccontStatement;
using API.Endpoints.AccountStatement.PutAccountStatements;

namespace API.Endpoints.AccountStatement;

public static class Accounts
{
    public static WebApplication MapAccountsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("accounts");

        group.MapPutAccountStatementsEndpoints()
            .MapGetAccountEndpoints();
        return app;
    }

    public static IServiceCollection AddAccountsServices(this IServiceCollection services)
    {
        services.AddPutAccountStatements()
            .AddGetAccount();
        
        return services;
    }
}