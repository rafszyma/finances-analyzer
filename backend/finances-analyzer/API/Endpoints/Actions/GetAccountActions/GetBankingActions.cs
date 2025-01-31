using Database.Extensions;

namespace API.Endpoints.Actions.GetAccountActions;

public static class GetActions
{
    public static RouteGroupBuilder MapGetActionsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("", async ([AsParameters]ActionFilter filter, GetBankingActionsQuery query) =>
            {
                var response = await query.Query(filter);

                return Results.Ok(response);
            })
            .DisableAntiforgery()
            .Produces<GetBankingActionsResponse>();

        return group;
    }

    public static IServiceCollection AddGetActions(this IServiceCollection services)
    {
        services.AddScoped<GetBankingActionsQuery>();
        
        return services;
    }
}