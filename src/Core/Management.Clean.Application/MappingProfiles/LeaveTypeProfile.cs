using AutoMapper;
using Management.Clean.Application.Features.LeaveType.Commands.CreateLeaveType;
using Management.Clean.Application.Features.LeaveType.Commands.UpdateLeaveType;
using Management.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Management.Clean.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using Management.Clean.Domain;

namespace Management.Clean.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}