using Management.Clean.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using Management.Clean.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using Management.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Management.Clean.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using Management.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using Management.Clean.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Clean.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestsQuery());

        return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        var query = new GetLeaveRequestDetailsQuery(id);
        var leaveRequest = await _mediator.Send(query);

        return Ok(leaveRequest);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand command)
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
        var command = new DeleteLeaveRequestCommand { Id = id };
        await _mediator.Send(command);

        return NoContent();
    }
}
