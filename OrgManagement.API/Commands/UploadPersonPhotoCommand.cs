using MediatR;

namespace OrgManagement.API.Commands;

public class UploadPersonPhotoCommand : IRequest<string>
{
    public Guid PersonId { get; }
    public byte[] PhotoContent { get; }
    public string PhotoFileName { get; }

    public UploadPersonPhotoCommand(Guid personId, byte[] photoContent, string photoFileName)
    {
        PersonId = personId;
        PhotoContent = photoContent;
        PhotoFileName = photoFileName;
    }
}