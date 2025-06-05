using AutoMapper;
using MediatR;
using OrgManagement.API.Commands;
using OrgManagement.DataServices.Repositories;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Handlers;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, OrganizationDto>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrganizationCommandHandler> _logger;

    public CreateOrganizationCommandHandler(
        IOrganizationRepository organizationRepository,
        IMapper mapper,
        ILogger<CreateOrganizationCommandHandler> logger)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OrganizationDto> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var dto = request.OrganizationCreateDto;

        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            ParentOrganizationId = dto.ParentOrganizationId
        };

        await _organizationRepository.AddAsync(org);
        _logger.LogInformation("Organization created with ID {OrganizationId}", org.Id);

        var orgDto = _mapper.Map<OrganizationDto>(org);
        return orgDto;
    }
}