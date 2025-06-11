using MediatR;
using PhonePlus.Application.Cross.SMTP;
using PhonePlus.Application.Ports.Credits;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class UpdateCreditStatusUseCase(
    ICreditRepository creditRepository,
    IStateRepository stateRepository,
    IUnitOfWork unitOfWork,
    ISmtpNotifier smtpNotifier,
    IUserRepository userRepository
    ) : IRequestHandler<UpdateCreditStateInputPort>
{
    public async Task Handle(UpdateCreditStateInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var credit = await creditRepository.FindByIdAsync(request.RequestData.Id);
            if (credit == null)
            {
                throw new BadHttpRequestException("Credit not found");
            }

            var state = await stateRepository.FindByIdAsync(request.RequestData.StateId);
            if (state == null)
            {
                throw new BadHttpRequestException("State not found");
            }
            
            var user = await userRepository.FindByIdAsync(credit.UserId);
            if (user == null)
            {
                throw new BadHttpRequestException("User not found");
            }

            credit.UpdateState(state.Id);
            creditRepository.Update(credit);
            await unitOfWork.CompleteAsync();
            string body = $"<html><body><h1>Credit Status Updated</h1><p>Your credit {(state.Id == 2 ? "just approved" : "just rejected")}.</p></body></html>";
            const string subject = "Credit Status Updated";
            const string from = "aljandro.jave@gmail.com";
            var to = user.Email;
            await smtpNotifier.SendNotification(body, subject, from,to);
            
            request.OutputPort.Handle(true);
        }
        catch (BadHttpRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}