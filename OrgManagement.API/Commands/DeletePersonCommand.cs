using MediatR;

namespace OrgManagement.API.Commands;

public class DeletePersonCommand : IRequest<bool>
{
    public Guid Id { get; }

    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }
}