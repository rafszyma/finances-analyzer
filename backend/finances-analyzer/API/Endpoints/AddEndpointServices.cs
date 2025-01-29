using API.Endpoints.AccountStatement.AddAccountStatements;

namespace API.Endpoints;

public static class ServicesRegister
{
    public static IServiceCollection AddEndpointServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddPutAccountStatements();
        return serviceCollection;
    }
}