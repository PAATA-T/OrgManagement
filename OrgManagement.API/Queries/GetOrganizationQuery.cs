using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Queries;

public class GetOrganizationQuery :IRequest<Organization>
{
    public Guid OrganizationId { get;}

    public GetOrganizationQuery(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
}