using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetEvent.Server.Data;
using NetEvent.Server.Middleware;
using NetEvent.Server.Models;
using NetEvent.Server.Modules;

var builder = WebApplication.CreateBuilder(args);

switch (builder.Configuration["DBProvider"].ToLower())
{
    case "sqlite":
        {
            builder.Services.AddDbContext<ApplicationDbContext, SqliteApplicationDbContext>();
            builder.Services.AddHealthChecks().AddDbContextCheck<SqliteApplicationDbContext>();
        }
        break;
    case "psql":
        {
            builder.Services.AddDbContext<ApplicationDbContext, PsqlApplicationDbContext>();
            builder.Services.AddHealthChecks().AddDbContextCheck<PsqlApplicationDbContext>();
        }
        break;
    default:
        {
            throw new Exception($"DbProvider not recognized: {builder.Configuration["DBProvider"]}");
        }
}

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<NetEventUserManager>()
    .AddRoleManager<NetEventRoleManager>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddSteam();

builder.Services.RegisterModules();

builder.Services.AddRouting(options => options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    using (var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
    {
        ctx.Database.Migrate();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHealthChecks("/healthz");
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseWebSockets();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToFile("index.html");
});

app.MapEndpoints();

await app.RunAsync();