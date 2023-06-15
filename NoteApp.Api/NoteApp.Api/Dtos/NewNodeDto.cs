namespace NoteApp.Api.Dtos;

public class NewNodeDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime Created { get; set; } // TODO: Make immutable in the database
    public DateTime LastModified { get; set; }
    public string UrlSuffix { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
}
