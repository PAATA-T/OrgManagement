using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Queries;

public class GetTopOrganizationQuery : IRequest<IEnumerable<Organization>>
{
    
}