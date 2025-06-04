using AutoMapper;
using MediatR;
using OrgManagement.API.Commands;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PersonsController(IPersonRepository personRepository, 
        IWebHostEnvironment environment, 
        IMapper mapper,
        IMediator mediator)
    {
        _personRepository = personRepository;
        _environment = environment;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPerson([FromBody] PersonCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var person = _mapper.Map<Person>(dto);

        await _personRepository.AddAsync(person);

        return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
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
            return BadRequest(ModelState);

        var existingPerson = await _personRepository.GetByIdAsync(id);
        if (existingPerson == null)
            return NotFound();

        existingPerson.FirstName = dto.FirstName;
        existingPerson.LastName = dto.LastName;
        existingPerson.PersonalNumber = dto.PersonalNumber;
        existingPerson.BirthDate = dto.BirthDate;
        existingPerson.ForeignLanguage = dto.ForeignLanguage;
        existingPerson.OrganizationId = dto.OrganizationId;

        await _personRepository.UpdateAsync(existingPerson);

        return NoContent();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person); 
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

        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
            return NotFound();

        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(fileStream);
        }

        person.PhotoUrl = Path.Combine("Uploads", uniqueFileName);
        await _personRepository.UpdateAsync(person);

        return Ok(new { PhotoPath = person.PhotoUrl });
    }
}