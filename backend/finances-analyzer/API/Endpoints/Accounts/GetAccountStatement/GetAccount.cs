using Database.Extensions;

namespace API.Endpoints.AccountStatement.GetAccontStatement;

public static class GetAccount
{
    public static RouteGroupBuilder MapGetAccountEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("{accountId}", async (int accountId, [AsParameters]ActionFilter filter, GetAccountQuery query) =>
            {
                var response = await query.Query(accountId, filter);

                return Results.Ok(response);
            })
            .DisableAntiforgery()
            .Produces<GetAccountResponse>();

        return group;
    }

    public static IServiceCollection AddGetAccount(this IServiceCollection services)
    {
        services.AddScoped<GetAccountQuery>();
        
        return services;
    }
}