using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Business;
using CoffeeShop.Business.AuthContext;
using CoffeeShop.Core;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.AuthContext.Configuration;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using CoffeeShop.Persistance.EntityFramework;
using CoffeeShop.Persistance.Repositories;
using Marten;
using Marten.Events.Projections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RiskFirst.Hateoas;
using System.Reflection;
using System.Text;
using Weasel.Core;

namespace CoffeeShop.Api.Configuration
{
    public static class DependenciesConfiguration
    {
        public static void AddHateoas(this IServiceCollection services)
        {
            services.AddTransient<IResourceMapper, ResourceMapper>();

            services.AddLinks(config =>
            {
                var policies = Assembly
                    .GetAssembly(typeof(DependenciesConfiguration))
                    .GetTypes()
                    .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == typeof(IPolicy<>).Name))
                    .Select(Activator.CreateInstance)
                    .Cast<dynamic>()
                    .ToArray();

                foreach (var policy in policies)
                {
                    config.AddPolicy(policy.PolicyConfiguration);
                }
            });
        }

        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IMenuItemsService, MenuItemsService>();
        }

        public static void AddCqrs(this IServiceCollection services)
        {
            services.AddScoped<IEventBus, EventBus>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            var repositoryTypes = Assembly
                .GetAssembly(typeof(IUserRepository))
                .GetTypes()
                .Where(t => t.Name.EndsWith("Repository"))
                .ToArray();

            var repositoryImplementationTypes = Assembly
                .GetAssembly(typeof(UserRepository))
                .GetTypes()
                .Where(t => t.Name.EndsWith("Repository"))
                .ToDictionary(t => t.Name, t => t);

            foreach (var repositoryType in repositoryTypes)
            {
                var expectedImplementationName = repositoryType
                    .Name
                    .Substring(1);

                if (!repositoryImplementationTypes.ContainsKey(expectedImplementationName))
                {
                    throw new InvalidOperationException($"Could not find implementation for {repositoryType.FullName}.");
                }

                var implementation = repositoryImplementationTypes[expectedImplementationName];

                if (!repositoryType.IsAssignableFrom(implementation))
                {
                    throw new InvalidOperationException($"For repository {repositoryType.Name} found matching type {implementation.Name}, but it does not implement it.");
                }

                services.AddTransient(repositoryType, implementation);
            }
        }

        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseNpgsql(connectionString));
        }

        public static void AddJwtIdentity(this IServiceCollection services, IConfigurationSection jwtConfiguration, Action<AuthorizationOptions> config)
        {
            services.AddTransient<IJwtFactory, JwtFactory>();

            services.AddIdentity<User, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            var signingKey = new SymmetricSecurityKey(
                Encoding.Default.GetBytes(jwtConfiguration["Secret"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtConfiguration[nameof(JwtConfiguration.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.Configure<JwtConfiguration>(options =>
            {
                options.Issuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)];
                options.Audience = jwtConfiguration[nameof(JwtConfiguration.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var queryAccessToken = context.Request.Query[AuthConstants.Queries.QueryParamAccessToken];
                        var cookieAccessToken = context.Request.Cookies[AuthConstants.Cookies.AuthCookieName];

                        if (!string.IsNullOrEmpty(queryAccessToken))
                        {
                            context.Token = queryAccessToken;
                        }
                        else if (!string.IsNullOrEmpty(cookieAccessToken))
                        {
                            context.Token = cookieAccessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config(options);
            });
        }

        public static void AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            var documentStore = DocumentStore.For(_ =>
            {
                var config = configuration.GetSection("EventStore");
                var connectionString = config.GetValue<string>("ConnectionString");
                var schemaName = config.GetValue<string>("Schema");

                _.Connection(connectionString);
                _.AutoCreateSchemaObjects = AutoCreate.All;
                _.Events.DatabaseSchemaName = schemaName;
                _.DatabaseSchemaName = schemaName;

               
                _.Projections.SelfAggregate<Tab>(ProjectionLifecycle.Inline);
                //_.Events.InlineProjections.AggregateStreamsWith<Tab>();
                _.Projections.Add(new TabViewProjection(), ProjectionLifecycle.Inline);

                var events = typeof(TabOpened)
                    .Assembly
                    .GetTypes()
                    .Where(t => typeof(IEvent).IsAssignableFrom(t))
                    .ToList();

                _.Events.AddEventTypes(events);
            });

            services.AddSingleton<IDocumentStore>(documentStore);

            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());
        }
    }
}