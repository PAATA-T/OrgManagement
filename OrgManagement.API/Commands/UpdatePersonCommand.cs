using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Commands;

public class UpdatePersonCommand : IRequest<bool>
{
    public Guid Id { get; }
    public PersonCreateDto PersonCreateDto { get; }

    public UpdatePersonCommand(Guid id, PersonCreateDto personCreateDto)
    {
        Id = id;
        PersonCreateDto = personCreateDto;
    }
}