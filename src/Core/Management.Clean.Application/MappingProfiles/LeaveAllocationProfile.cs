using AutoMapper;
using Management.Clean.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using Management.Clean.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using Management.Clean.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using Management.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using Management.Clean.Domain;

namespace Management.Clean.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}