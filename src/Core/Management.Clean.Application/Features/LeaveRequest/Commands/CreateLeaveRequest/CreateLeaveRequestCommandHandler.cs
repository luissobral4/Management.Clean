using AutoMapper;
using Management.Clean.Application.Contracts.Email;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Models.Emails;
using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;


    public CreateLeaveRequestCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CreateLeaveRequestCommandHandler> logger
    )
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
    }
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException(ValidationMessages.ObjectInvalid(nameof(LeaveRequest)), validationResult);

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var leaveRequestToCreate = _mapper.Map<Domain.LeaveRequest>(request);

        await _leaveRequestRepository.CreateAsync(leaveRequestToCreate);

         try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Subject = EmailParams.SubjectRequestSubmitted,
                Body = EmailParams.BodySubmited(request.StartDate, request.EndDate)
            };

            await _emailSender.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return Unit.Value;
    }
}