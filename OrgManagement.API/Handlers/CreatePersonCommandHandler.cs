using AutoMapper;
using MediatR;
using OrgManagement.API.Commands;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var personEntity = _mapper.Map<Person>(request.PersonCreateDto);

        if (personEntity.Id == Guid.Empty)
            personEntity.Id = Guid.NewGuid();

        await _personRepository.AddAsync(personEntity);
        
        var personDto = _mapper.Map<PersonDto>(personEntity);

        return personDto;
    }
}
