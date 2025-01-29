using API.Endpoints;
using Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointServices();
builder.Services.AddDatabase(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.MapEndpoint();

app.Run();