using API.Endpoints.AccountStatement;

namespace API.Endpoints;

public static class ServicesRegister
{
    public static IServiceCollection AddEndpointServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAccountsServices()
            .AddActionsServices();
        
        return serviceCollection;
    }
}