using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.ReadAccess, builder =>
        builder.RequireClaim("scope", "games:read"));

    options.AddPolicy(Policies.WriteAccess, builder =>
        builder.RequireClaim("scope", "games:write")
               .RequireRole("Admin"));
});

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapGamesEndpoints();

app.Run();
