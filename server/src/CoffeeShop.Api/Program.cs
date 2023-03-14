using CoffeeShop.Api.Configuration;
using CoffeeShop.Api.Filters;
using CoffeeShop.Api.Hateoas.Resources.Tab;
using CoffeeShop.Api.Hubs;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.AuthContext.Commands;
using CoffeeShop.Core.AuthContext.Configuration;
using CoffeeShop.Persistance;
using CoffeeShop.Persistance.EntityFramework;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddServices(builder);

        var app = builder.Build();

        ConfigureRequestPipeline(app);

        SeedDatabase(app);

        app.Run();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        var connstr = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                 //connstr, x => x.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name)));
                 builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception(),
                 x => x.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name)));

        builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(TabMappingProfile));
        builder.Services.AddCommonServices();
        builder.Services.AddHateoas();

        builder.Services.AddJwtIdentity(builder.Configuration.GetSection(nameof(JwtConfiguration)), options =>
            {
                options.AddPolicy(AuthConstants.Policies.IsAdmin, pb => pb.RequireClaim(AuthConstants.ClaimTypes.IsAdmin));

                options.AddPolicy(
                    AuthConstants.Policies.IsAdminOrManager,
                    pb => pb.IsAdminOr(ctx => ctx.User.HasClaim(c => c.Type == AuthConstants.ClaimTypes.ManagerId)));

                options.AddPolicy(
                    AuthConstants.Policies.IsAdminOrWaiter,
                    pb => pb.IsAdminOr(ctx => ctx.User.HasClaim(c => c.Type == AuthConstants.ClaimTypes.WaiterId)));

                options.AddPolicy(
                    AuthConstants.Policies.IsAdminOrCashier,
                    pb => pb.IsAdminOr(ctx => ctx.User.HasClaim(c => c.Type == AuthConstants.ClaimTypes.CashierId)));

                options.AddPolicy(
                    AuthConstants.Policies.IsAdminOrBarista,
                    pb => pb.IsAdminOr(ctx => ctx.User.HasClaim(c => c.Type == AuthConstants.ClaimTypes.BaristaId)));
            });
        builder.Services.AddMarten(builder.Configuration);
        builder.Services.AddCqrs();
        builder.Services.AddMediatR();
        builder.Services.AddSignalR();
        builder.Services.AddRepositories();

        builder.Services.AddMvc(options =>
        {
            options.Filters.Add<ExceptionFilter>();
            options.Filters.Add<ModelStateFilter>();
        });

        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
    }

    private static void ConfigureRequestPipeline(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder => builder
                 .SetIsOriginAllowedToAllowWildcardSubdomains()
                 .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:5000")
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.MapHub<ConfirmedOrdersHub>("/confirmedOrders");
        app.MapHub<HiredWaitersHub>("/hiredWaiters");
        app.MapHub<TableActionsHub>("/tableActions");
    }

    private static void SeedDatabase(WebApplication app)
    {
        DatabaseConfiguration.EnsureEventStoreIsCreated(app.Configuration);

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
                databaseInitializer.SeedDatabase().Wait();
                
            }
            catch (Exception)
            {
                //throw ex;
            }
        }
    }
}