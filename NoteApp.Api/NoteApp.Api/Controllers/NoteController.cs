using Microsoft.AspNetCore.Mvc;
using NoteApp.Api.Data;
using NoteApp.Api.Dtos;
using NoteApp.Api.Services;

namespace NoteApp.Api.Controllers;

[ApiController]
[Route("Note")]
public class NoteController : ControllerBase
{
    private readonly NoteService _noteService;

    public NoteController(NoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet("ByNoteId")]
    public async Task<Notes?> GetNote(int noteId)
    {
        return await _noteService.GetNoteAsync(noteId);
    }

    [HttpGet("ByUrlSuffix")]
    public async Task<Notes?> GetNote(string urlSuffix)
    {
        return await _noteService.GetNoteAsync(urlSuffix);
    }

    [HttpPost("CreateOrEditNote")]
    public async Task<Notes> CreateOrEditNote(NoteDto note)
    {
        return await _noteService.CreateOrEditNoteAsync(note);
    }

    [HttpGet("GetUsersNotes")]
    public async Task<List<Notes>> GetUsersNotes(string appUserId, int page)
    {
        return await _noteService.GetUsersNotesAsync(appUserId, page);
    }

    [HttpPost("DeleteNote")]
    public async Task<int> RemoveNote(int noteId)
    {
        return await _noteService.RemoveNoteAsync(noteId);
    }
}
