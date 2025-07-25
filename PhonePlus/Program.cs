
using PhonePlus.Application.Ports.Auth;
using PhonePlus.Application.Ports.Auth.Output;
using PhonePlus.Application.Ports.Credits;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Application.Ports.Credits.Output;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Interface.DTO.Auth;
using PhonePlus.Interface.DTO.Credits;
using PhonePlus.IOC;
using AuthorizationMiddleware = PhonePlus.Interface.Middleware.AuthorizationMiddleware;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyContainer(configuration, builder);

var app = builder.Build();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    
    
    var seedRoleUseCase =  services.GetRequiredService<MediatR.IMediator>();
    var seedRolesDto = new SeedRolesDto();
    var outPutPort = new SeedRolesOutputPort();
    await seedRoleUseCase.Send(new SeedRolesInputPort(seedRolesDto,outPutPort ));

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors("AllowAllOrigins");
app.UseMiddleware<AuthorizationMiddleware>();

app.Run();
