using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Api.Data;

[Index(nameof(UserName), IsUnique = true)]
public class AppUser : IdentityUser
{
    public required string Name { get; set; } // TODO: Change Name to 'DisplayName'?
    public int NumberOfNotes { get; set; }
    public ICollection<Notes>? Notes { get; set; }
}
