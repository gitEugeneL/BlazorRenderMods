using Application;
using InteractiveSSRMode.Components;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

/*** Register services ***/
builder.Services
    .AddApplicationServices()
    .AddPersistenceServices(builder.Configuration);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();