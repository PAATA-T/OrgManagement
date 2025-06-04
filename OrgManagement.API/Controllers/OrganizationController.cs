using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;
namespace OrgManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<OrganizationController> _logger;
    private readonly IMediator _mediator;

    public OrganizationController(IOrganizationRepository organizationRepository, 
        IMapper mapper, 
        ILogger<OrganizationController> logger, 
        IMediator mediator)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OrganizationCreateDto dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            ParentOrganizationId = dto.ParentOrganizationId
        };

        await _organizationRepository.AddAsync(org);
        _logger.LogInformation("Organization created", org.Id);
        
        return CreatedAtAction(nameof(GetById), new { id = org.Id }, org);
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