using API.Endpoints.Accounts.GetAccount;
using API.Endpoints.Accounts.PatchAccount;
using API.Endpoints.Accounts.PutAccountStatements;

namespace API.Endpoints.Accounts;

public static class Accounts
{
    public static WebApplication MapAccountsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("accounts");

        group.MapPutAccountStatementsEndpoints()
            .MapGetAccountEndpoints()
            .MapPatchAccountEndpoints();
        return app;
    }

    public static IServiceCollection AddAccountsServices(this IServiceCollection services)
    {
        services.AddPutAccountServices()
            .AddGetAccountServices()
            .AddPatchAccountServices();
        
        return services;
    }
}