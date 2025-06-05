using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Commands;

public class CreatePersonCommand : IRequest<PersonDto>
{
    public PersonCreateDto PersonCreateDto { get; }

    public CreatePersonCommand(PersonCreateDto personCreateDto)
    {
        PersonCreateDto = personCreateDto;
    }
}