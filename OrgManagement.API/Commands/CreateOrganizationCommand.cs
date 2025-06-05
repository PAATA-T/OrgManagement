using MediatR;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Commands;

public class CreateOrganizationCommand : IRequest<OrganizationDto>
{
    public OrganizationCreateDto OrganizationCreateDto { get; }

    public CreateOrganizationCommand(OrganizationCreateDto dto)
    {
        OrganizationCreateDto = dto;
    }
}