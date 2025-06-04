using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Queries;

public class GetPersonByIdQuery :IRequest<PersonDto>
{
    public Guid Id { get; }

    public GetPersonByIdQuery(Guid id)
    {
        Id = id;
    }
}
