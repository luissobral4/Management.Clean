using Management.Clean.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using Management.Clean.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using Management.Clean.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using Management.Clean.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using Management.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.Clean.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());

        return Ok(leaveAllocations);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        var query = new GetLeaveAllocationDetailsQuery(id);
        var leaveAllocation = await _mediator.Send(query);

        return Ok(leaveAllocation);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await _mediator.Send(command);

        return NoContent();
    }
}
