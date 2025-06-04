using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Queries;

public class GetAllOrganizationQuery :IRequest<IEnumerable<Organization>>
{
    
}