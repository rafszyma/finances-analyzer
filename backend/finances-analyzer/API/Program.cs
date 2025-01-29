using API.Endpoints;
using Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointServices();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddSerilog();
builder.Services.AddLogging();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register( () =>
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<FinancesDbContext>();
    db.Database.Migrate();
});
app.MapOpenApi();
app.UseHttpsRedirection();
app.MapEndpoint();

app.Run();