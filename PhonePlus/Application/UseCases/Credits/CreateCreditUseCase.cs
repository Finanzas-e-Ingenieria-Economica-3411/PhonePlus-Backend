using MediatR;
using PhonePlus.Application.Cross.SMTP;
using PhonePlus.Application.Ports.Credits;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class CreateCreditUseCase(
    ICreditRepository creditRepository, 
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    ISmtpNotifier smtpNotifier
    ) : IRequestHandler<CreateCreditInputPort>
{
    public async Task Handle(CreateCreditInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.FindByIdAsync(request.RequestData.UserId);
            if (user == null)
            {
                throw new BadHttpRequestException("User not found");
            }

            var credit = new Credit(request.RequestData);

            await creditRepository.AddAsync(credit);
            await unitOfWork.CompleteAsync();
            const string body = "<html><body><h1>Credit Created</h1><p>Your credit has been created successfully.</p></body></html>";
            const string subject = "Credit Created";
            var to = user.Email;
            const string from = "aljandro.jave@gmail.com";
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