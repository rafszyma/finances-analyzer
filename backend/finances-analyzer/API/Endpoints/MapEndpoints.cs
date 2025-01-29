using API.Endpoints.AccountStatement;

namespace API.Endpoints;

public static class MapEndpoints
{
    public static WebApplication MapEndpoint(this WebApplication app)
    {
        app.MapAccountStatementEndpoints();

        return app;
    }
}

