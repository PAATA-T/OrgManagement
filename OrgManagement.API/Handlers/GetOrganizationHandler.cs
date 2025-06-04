using MediatR;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class GetOrganizationHandler : IRequestHandler<GetOrganizationQuery, Organization>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        
        var org = await _organizationRepository.GetByIdAsync(request.OrganizationId);
        
        return org == null ? null : org;
    }
}