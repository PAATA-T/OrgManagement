namespace OrgManagement.API.Controllers;
using MediatR;
using Commands;
using Queries;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonsController> _logger;

    public PersonsController(IMediator mediator, ILogger<PersonsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPerson([FromBody] PersonCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreatePersonCommand(dto);
        var resultDto = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetPersonById), new { id = resultDto.Id }, resultDto);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var command = new DeletePersonCommand(id);

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] PersonCreateDto dto)
    {
        if (!ModelState.IsValid)
            //_logger.LogInformation("Model state is invalid");
            return BadRequest(ModelState);

        var success = await _mediator.Send(new UpdatePersonCommand(id, dto));
        if (!success)
            return NotFound();

        return NoContent();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        var dto = await _mediator.Send(new GetPersonByIdQuery(id));
        if (dto == null)
            return NotFound();

        return Ok(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPersons([FromQuery] Guid? organizationId, 
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllPersonQuery()
        {
            OrganizationId = organizationId,
            Page = page,
            PageSize = pageSize
        };

        var response = await _mediator.Send(query);

        return Ok(response);
    }
    
    [HttpPost("{id}/upload-photo")]
    public async Task<IActionResult> UploadPhoto(Guid id, IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
            return BadRequest("Photo file is missing.");

        byte[] photoBytes;
        using (var ms = new MemoryStream())
        {
            await photo.CopyToAsync(ms);
            photoBytes = ms.ToArray();
        }

        var command = new UploadPersonPhotoCommand(id, photoBytes, photo.FileName);
        var photoPath = await _mediator.Send(command);

        return Ok(new { PhotoPath = photoPath });
    }
}