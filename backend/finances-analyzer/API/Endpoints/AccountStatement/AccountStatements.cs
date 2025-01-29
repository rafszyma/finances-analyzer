using API.Endpoints.AccountStatement.AddAccountStatements;

namespace API.Endpoints.AccountStatement;

public static class AccountStatements
{
    public static WebApplication MapAccountStatementEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("account-statements");

        group.MapPutAccountStatementsEndpoints();
        return app;
    }

    public static IServiceCollection AddAccountStatements(this IServiceCollection services)
    {
        services.AddPutAccountStatements();
        
        return services;
    }
}