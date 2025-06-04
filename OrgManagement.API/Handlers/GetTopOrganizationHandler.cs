using MediatR;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class GetTopOrganizationHandler : IRequestHandler<GetTopOrganizationQuery, IEnumerable<Organization>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetTopOrganizationHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    
    public async Task<IEnumerable<Organization>> Handle(GetTopOrganizationQuery request, CancellationToken cancellationToken)
    {
        var topOrgs = await _organizationRepository.GetTopLevelOrganizationsAsync();

        return topOrgs;
    }
}