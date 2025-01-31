using API.Endpoints.Accounts;
using API.Endpoints.Actions;

namespace API.Endpoints;

public static class MapEndpoints
{
    public static WebApplication MapEndpoint(this WebApplication app)
    {
        app.MapAccountsEndpoints()
            .MapActionsEndpoints();

        return app;
    }
}

