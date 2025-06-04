using MediatR;
using OrgManagement.API.Queries;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class GetAllOrganizationsHandler : IRequestHandler<GetAllOrganizationQuery, IEnumerable<Organization>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetAllOrganizationsHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    
    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organizations = await _organizationRepository.GetOrganizationTreeAsync();

        return organizations;
    }
}