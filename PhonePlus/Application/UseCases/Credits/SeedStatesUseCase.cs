using System.ComponentModel;
using MediatR;
using PhonePlus.Application.Ports.Credits;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Enums;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class SeedStatesUseCase(IStateRepository stateRepository, IUnitOfWork unitOfWork) : IRequestHandler<SeedStatesInputPort>
{
    public async Task Handle(SeedStatesInputPort request, CancellationToken cancellationToken)
    {
        foreach (States state in Enum.GetValues(typeof(States)))
        {
            var description = state.GetType()
                .GetField(state.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description;

            if (description != null && !await stateRepository.IsStateExistsAsync(description))
            {
                Console.WriteLine($"State {description} does not exist");
                var newState = new State(description);
                await stateRepository.AddAsync(newState);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}