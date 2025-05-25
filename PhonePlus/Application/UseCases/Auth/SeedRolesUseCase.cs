using System.ComponentModel;
using MediatR;
using PhonePlus.Application.Ports.Auth;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Enums;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Auth;

public sealed class SeedRolesUseCase(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<SeedRolesInputPort>
{
    
    public async Task Handle(SeedRolesInputPort request, CancellationToken cancellationToken)
    {
        foreach (RoleTypes role in Enum.GetValues(typeof(RoleTypes)))
        {
            var description = role.GetType()
                .GetField(role.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description;

            if (description != null && !await roleRepository.IsRoleExists(description))
            {
                Console.WriteLine($"Role {description} does not exist");
                var newRole = new UserRole(description);
                await roleRepository.AddAsync(newRole);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}