using System.Collections;
using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Queries;

public class GetAllPersonQuery :IRequest<GetPersonsResponse>
{
    public Guid? OrganizationId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPersonsResponse
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<PersonDto> Items { get; set; }
}