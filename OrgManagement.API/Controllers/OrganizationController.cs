using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgManagement.API.Commands;
using OrgManagement.API.Queries;
using OrgManagement.Entities.Models;
namespace OrgManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly ILogger<OrganizationController> _logger;
    private readonly IMediator _mediator;

    public OrganizationController( 
        ILogger<OrganizationController> logger, 
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OrganizationCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateOrganizationCommand(dto);
        var orgDto = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = orgDto.Id }, orgDto);
    }

    [HttpGet("tree")]
    public async Task<IActionResult> GetTree()
    {
        var query = new GetAllOrganizationQuery();
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [HttpGet("top-level")]
    public async Task<IActionResult> GetTopLevel()
    {
        var query = new GetTopOrganizationQuery();
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetOrganizationQuery(id);
        
        var result = await _mediator.Send(query);
        
        if (result == null) return NotFound();
        
        return Ok(result);
    }
}