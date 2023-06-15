using Microsoft.AspNetCore.Mvc;
using Noted.Api.Data;
using Noted.Api.Dtos;
using Noted.Api.Services;

namespace Noted.Api.Controllers
{
    [ApiController]
    [Route("Note")]
    public class NoteController : ControllerBase
    {
        // hi
        private readonly NoteService _noteService;
        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("Get")]
        public IEnumerable<Note> GetNotes()
        {
            return _noteService.GetNotes();
        }

        [HttpGet("GetNoteById")]
        public Note GetNoteById(Guid id)
        {
            return _noteService.GetNoteById(id);
        }

        [HttpGet("GetDeletedNotes")]
        public IEnumerable<Note> GetDeletedNotes(Guid id)
        {
            return _noteService.GetDeletedNotes();
        }

        [HttpPost("Add")]
        public void AddNote([FromBody] NoteDto note)
        {
            _noteService.AddNote(note);
        }

        [HttpPut("Update")]
        public void UpdateNote([FromBody] NoteDto note)
        {
            _noteService.UpdateNote(note);
        }

        [HttpDelete("Delete")]
        public void DeleteNote(Guid id)
        {
            _noteService.DeleteNote(id);
        }

        [HttpPut("RestoreNote")]
        public void RestoreNote(Guid id)
        {
            _noteService.RestoreNote(id);
        }
    }
}
