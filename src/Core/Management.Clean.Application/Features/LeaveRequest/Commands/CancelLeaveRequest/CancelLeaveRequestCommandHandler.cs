using Management.Clean.Application.Contracts.Email;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Models.Emails;
using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;

    public CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CancelLeaveRequestCommandHandler> logger
    )
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
    }
    public async Task Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequestToCancel = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequestToCancel == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequestToCancel.Cancelled = true;
        await _leaveRequestRepository.UpdateAsync(leaveRequestToCancel);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Subject = EmailParams.SubjectRequestCancelled,
                Body = EmailParams.BodyCancelled(leaveRequestToCancel.StartDate, leaveRequestToCancel.EndDate)
            };

            await _emailSender.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }
    }
}