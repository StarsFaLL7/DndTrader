using Application;
using DndTraderServer.Components;
using Infrastructure;
using ItemParserWorker;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
    .CreateLogger();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(c => c.DetailedErrors = true);



builder.Services.TryAddApplication();
builder.Services.TryAddInfrastructure();
builder.Services.AddScoped<ItemsParser>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    InfrastructureStartup.CheckAndMigrateDatabase(scope);
}

app.Run();

Log.CloseAndFlush();