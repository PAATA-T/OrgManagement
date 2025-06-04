using AutoMapper;
using MediatR;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonByIdHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        if (person == null)
            return null;  

        return _mapper.Map<PersonDto>(person);
    }
}