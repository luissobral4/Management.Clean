using AutoMapper;
using Management.Clean.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using Management.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Management.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using Management.Clean.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using Management.Clean.Domain;

namespace Management.Clean.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
        CreateMap<ChangeLeaveRequestApprovalCommand, LeaveRequest>();
    }
}