using AutoMapper;
using MediatR;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class GetAllPersonHandler : IRequestHandler<GetAllPersonQuery, GetPersonsResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAllPersonHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetPersonsResponse> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page > 0 ? request.Page : 1;
        var pageSize = request.PageSize > 0 ? request.PageSize : 10;
        
        var (persons, totalCount) = await _personRepository.GetPersonsAsync(request.OrganizationId, page, pageSize);
        
        var personsDto = _mapper.Map<IEnumerable<PersonDto>>(persons);
        
        return new GetPersonsResponse
        {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Items = personsDto
        };
    }
}