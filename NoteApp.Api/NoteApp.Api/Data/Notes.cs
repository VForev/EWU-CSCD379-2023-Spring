using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Api.Data;

[Index(nameof(UrlSuffix), IsUnique = true)]
public class Notes
{
    [Key]
    public int NoteId { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public required string UrlSuffix { get; set; }
    public required string AppUserId { get; set; }
}
