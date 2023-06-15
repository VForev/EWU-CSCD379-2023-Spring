using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Data;

namespace NoteApp.Api.Services;

public class UrlService
{
    private readonly AppDbContext _db;

    public UrlService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<string> GenerateUniqueUrlSuffixAsync(int suffixLength = 12)
    {
        var suffix = GenerateRandomString(suffixLength);

        while (await IsUrlSuffixExistsInDatabaseAsync(suffix))
        {
            suffix = GenerateRandomString(suffixLength);
        }

        return suffix;
    }

    private async Task<bool> IsUrlSuffixExistsInDatabaseAsync(string suffix)
    {
        return await _db.Notes.AnyAsync(n => n.UrlSuffix == suffix);
    }

    private string GenerateRandomString(int suffixLength)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, suffixLength).Select(s => s[random.Next(s.Length)]).ToArray());
        return result;
    }
}
