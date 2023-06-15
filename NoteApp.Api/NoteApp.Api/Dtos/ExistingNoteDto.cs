namespace NoteApp.Api.Dtos;

public class ExistingNoteDto
{
    public int NoteId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime LastModified { get; set; }
    public string UrlSuffix { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
}
