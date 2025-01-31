using API.Endpoints.Accounts;
using API.Endpoints.Actions;

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