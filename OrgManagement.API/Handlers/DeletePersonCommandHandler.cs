using MediatR;
using OrgManagement.API.Commands;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.DataServices.Repositories.Implementation;

namespace OrgManagement.API.Handlers;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var existingPerson = await _personRepository.GetByIdAsync(request.Id);
        if (existingPerson == null)
        {
            return false;
        }

        await _personRepository.DeleteAsync(request.Id);

        return true; 
    }
}