using AutoMapper;
using Management.Clean.Application.Contracts.Email;
using Management.Clean.Application.Contracts.Identity;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Models.Emails;
using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IUserService _userService;


    public CreateLeaveRequestCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CreateLeaveRequestCommandHandler> logger,
        IUserService userService,
        ILeaveAllocationRepository leaveAllocationRepository
    )
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
        _userService = userService;
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException(ValidationMessages.ObjectInvalid(nameof(LeaveRequest)), validationResult);

        var employeeId = _userService.UserId;

        var allocation = await GetUserAllocation_ThowExceptionIfDontExistsAsync(request, validationResult, employeeId);

        ValidateDaysRequested_ThrowExceptionIfInvalid(request, validationResult, allocation);

        var leaveRequestToCreate = _mapper.Map<Domain.LeaveRequest>(request);
        leaveRequestToCreate.RequestingEmployeeId = employeeId;
        leaveRequestToCreate.DateRequested = DateTime.Now;
        var id = await _leaveRequestRepository.CreateAsync(leaveRequestToCreate);

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

        return id;
    }

    private async Task<Domain.LeaveAllocation> GetUserAllocation_ThowExceptionIfDontExistsAsync(CreateLeaveRequestCommand request, FluentValidation.Results.ValidationResult validationResult, string employeeId)
    {
        var allocation = await _leaveAllocationRepository.GetUserAllocationsAsync(employeeId, request.LeaveTypeId);

        if (allocation is null)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                nameof(request.LeaveTypeId),
                ValidationMessages.RequestAllocationsIsNull)
            );
            throw new BadRequestException(ValidationMessages.ObjectInvalid(nameof(LeaveRequest)), validationResult);
        }

        return allocation;
    }

    private static void ValidateDaysRequested_ThrowExceptionIfInvalid(CreateLeaveRequestCommand request, FluentValidation.Results.ValidationResult validationResult, Domain.LeaveAllocation allocation)
    {
        var daysRequested = HelperFunctions.CalculateRequestedDays(request.StartDate, request.EndDate);

        if (daysRequested > allocation.NumberOfDays)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                nameof(request.EndDate),
                ValidationMessages.RequestDaysInvalid)
            );
            throw new BadRequestException(ValidationMessages.ObjectInvalid(nameof(LeaveRequest)), validationResult);

        }
    }
}