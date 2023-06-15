using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Api.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Notes> Notes => Set<Notes>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}
