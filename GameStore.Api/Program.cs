using System.Diagnostics;
using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.ErrorHandling;
using GameStore.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();

// Add Versioning using Asp.Versioning.Http
builder.Services.AddApiVersioning();

// Add HTTP Logging
builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());

app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDbAsync();

// Add HTTP Logging
app.UseHttpLogging();

app.MapGamesEndpoints();

app.Run();
