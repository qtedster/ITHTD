using ITHTD.Components;
using ITHTD.Data;
using ITHTD.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<HelpdeskContext>(options =>
    options.UseSqlite("Data Source=helpdesk.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<HelpdeskContext>>();
    await using var db = await dbFactory.CreateDbContextAsync();

    if (!db.Agents.Any())
    {
        db.Agents.AddRange(
            new Agent { Name = "Kwame Asante" },
            new Agent { Name = "Ama Owusu" }
        );
        await db.SaveChangesAsync();
    }

    if (!db.Tickets.Any())
    {
        var agent = db.Agents.First();
        db.Tickets.AddRange(
            new Ticket { Title = "Printer not connecting", Description = "3rd floor printer offline since morning.", RequesterName = "Yaw Boateng", Priority = TicketPriority.Medium },
            new Ticket { Title = "Email account locked", Description = "Too many failed login attempts.", RequesterName = "Efua Mensah", Priority = TicketPriority.High, AssignedAgentId = agent.Id },
            new Ticket { Title = "VPN access request", Description = "New hire needs remote access setup.", RequesterName = "Kojo Appiah", Priority = TicketPriority.Low }
        );
        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();