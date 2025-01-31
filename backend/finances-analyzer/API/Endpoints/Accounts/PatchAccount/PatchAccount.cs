using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Accounts.PatchAccount;

public static class PatchAccount
{
    public static RouteGroupBuilder MapPatchAccountEndpoints(this RouteGroupBuilder group)
    {
        group.MapPatch("{accountId}", async ([FromRoute]int accountId, [FromBody]PatchAccountRequest request, PatchAccountCommand command) =>
        {
            request.Id = accountId;
            await command.Execute(request);

            return Results.Ok();
        });

        return group;
    }

    public static IServiceCollection AddPatchAccountServices(this IServiceCollection services)
    {
        services.AddScoped<PatchAccountCommand>();
        
        return services;
    }
}