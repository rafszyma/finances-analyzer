using API.Endpoints.AccountStatement.GetAccountActions;

namespace API.Endpoints.AccountStatement;

public static class Actions
{
    public static WebApplication MapActionsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("actions");

        group.MapGetActionsEndpoints();
        return app;
    }

    public static IServiceCollection AddActionsServices(this IServiceCollection services)
    {
        services.AddGetActions();
        
        return services;
    }
}