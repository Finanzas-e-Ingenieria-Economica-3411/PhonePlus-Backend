using MediatR;
using Microsoft.EntityFrameworkCore;
using PhonePlus.Application.Cross.SMTP;
using PhonePlus.Common.Auth;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Repositories;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Infrastructure.Cross.SMTP;
using PhonePlus.Infrastructure.Repositories.Common;
using PhonePlus.Infrastructure.Repositories.Credits;
using PhonePlus.Infrastructure.Repositories.Notifications;
using PhonePlus.Infrastructure.Repositories.Role;
using PhonePlus.Infrastructure.Repositories.Users;

namespace PhonePlus.IOC;

public static class InfrastructureContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration,WebApplicationBuilder builder)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var productionConnectionString = configuration.GetConnectionString("ProductionConnection");
        services.AddDbContext<AppDbContext>(
            options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
                else if (builder.Environment.IsProduction())
                {
                    options.UseMySql(productionConnectionString, ServerVersion.AutoDetect(productionConnectionString)) //Use this when docker compose would create
                        .LogTo(Console.WriteLine, LogLevel.Error)
                        .EnableDetailedErrors();
                }
            });

                services.AddSingleton(configuration);
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISmtpNotifier, SmtpNotifier>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<ICreditRepository, CreditRepository>();
        

        return services;




    }

}