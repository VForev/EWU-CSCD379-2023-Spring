using Noted.Api.Data;
using Noted.Api.Dtos;

namespace Noted.Api.Services
{
    public class NoteService
    {
        private readonly AppDbContext _db;

        public NoteService(AppDbContext db)
        {
            _db = db;
        }
      // fsd
        public IEnumerable<Note> GetNotes() 
        {
            return _db.Notes.Where(note => note.deleted == false).ToList();
        }

        public Note GetNoteById(Guid id)
        {
            return _db.Notes.Where(note => note.Id == id).First();
        }

        public IEnumerable<Note> GetDeletedNotes()
        {
            return _db.Notes.Where(note => note.deleted == true).ToList();
        }
        public void AddNote(NoteDto note)
        {
            if(note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            var newNote = new Note();
            
            if(note.Title == "")
            {
                note.Title = "Untitled Note";
            }
            newNote.Title = note.Title;
            newNote.Content = note.Content;
            newNote.deleted = false;
            
            _db.Notes.Add(newNote);
            _db.SaveChanges();   
        }

        public void UpdateNote(NoteDto note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            if (note.Title == "")
            {
                note.Title = "Note";
            }

            var storedNote = _db.Notes.Select(n => n).Where(n => n.Id == note.Id).First();

            if (storedNote == null)
            {
                throw new Exception("Id not found");
            }

            storedNote.Title = note.Title;
            storedNote.Content = note.Content;
            
            _db.SaveChanges();
        }

        public void DeleteNote(Guid id)
        {

            var storedNote = _db.Notes.Where(n => n.Id == id).First();

            if (storedNote == null)
            {
                throw new Exception("Id not found");
            }

            storedNote.deleted = true;
            storedNote.DeletedDate = DateTime.Now;

            _db.SaveChanges();
        }

        public void RestoreNote(Guid id)
        {
            var noteToRestore = _db.Notes.Where(n => n.Id == id).First();

            if (noteToRestore == null)
            {
                throw new Exception("Id not found");
            }

            noteToRestore.deleted = false;

            _db.SaveChanges();
        }
    }
}
