namespace NoteApp.Api.Dtos;

public class NoteDto
{
    public int NoteId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    // public string UrlSuffix { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
}
