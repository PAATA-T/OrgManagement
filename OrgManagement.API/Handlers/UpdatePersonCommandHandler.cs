using AutoMapper;
using MediatR;
using OrgManagement.API.Commands;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var existingPerson = await _personRepository.GetByIdAsync(request.Id);
        if (existingPerson == null)
        {
            return false;  
        }
        
        var result = _mapper.Map(request.PersonCreateDto, existingPerson);

        await _personRepository.UpdateAsync(result);

        return true; 
    }
}