using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Data;
using NoteApp.Api.Dtos;

namespace NoteApp.Api.Services;

public class NoteService
{
    private readonly AppDbContext _db;

    public NoteService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Notes?> GetNoteAsync(int noteId)
    {
        return await _db.Notes.FirstOrDefaultAsync(w => w.NoteId == noteId);
    }

    public async Task<Notes?> GetNoteAsync(string urlSuffix)
    {
        return await _db.Notes.FirstOrDefaultAsync(w => w.UrlSuffix == urlSuffix);
    }

    public async Task<Notes> CreateOrEditNoteAsync(NoteDto note)
    {
        if (note == null)
        {
            throw new ArgumentNullException("The note is null");
        }

        var savedNote =
            await _db.Notes.FirstOrDefaultAsync(w => w.NoteId == note.NoteId && w.AppUserId == note.AppUserId);
        var urlService = new UrlService(_db);

        if (savedNote != null)
        {
            savedNote.Title = note.Title;
            savedNote.Content = note.Content;
            savedNote.LastModified = DateTime.Now;
        }
        else
        {
            savedNote = new() { AppUserId = note.AppUserId,
                                Title = note.Title,
                                Content = note.Content,
                                LastModified = DateTime.Now,
                                UrlSuffix = urlService.GenerateUniqueUrlSuffixAsync().Result,
                                Created = DateTime.Now };
            _db.Notes.Add(savedNote);
        }

        await _db.SaveChangesAsync();
        return savedNote;
    }

    public async Task<List<Notes>> GetUsersNotesAsync(string appUserId, int page)
    {
        return await _db.Notes.Select(n => n)
            .Where(n => n.AppUserId == appUserId)
            .Skip(10 * page)
            .Take(10)
            .ToListAsync();
    }

    public async Task<int> RemoveNoteAsync(int noteId)
    {
        return await _db.Notes.Select(n => n).Where(n => n.NoteId == noteId).ExecuteDeleteAsync();
    }
}
