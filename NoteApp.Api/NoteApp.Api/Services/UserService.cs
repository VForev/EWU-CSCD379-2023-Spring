using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Data;

namespace NoteApp.Api.Services;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<string> GetUserIdAsync(string username)
    {
        var user = await _db.AppUsers.FirstOrDefaultAsync(w => w.UserName == username);

        // TODO: Add a check for null user (Warns of 'Dereference of a possibly null reference')
        return user.Id;
    }
}
