using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface INotesManager
    {
        Task<string> AddNewNote(NotesModel notesModel);
        Task<string> DeleteNotes(long NoteId, long UserId);
        Task<List<NotesModel>> ShowAllNotes(long UserId);
        Task<string> UpdateNote(NotesModel notesModel);
        Task<string> ChangeColor(NotesModel notesModel);
        Task<string> CheckArchive(NotesModel notesModel);
        Task<string> CheckPin(NotesModel notesModel);
        Task<string> TrashNote(NotesModel notesModel);
        Task<List<NotesModel>> ArchiveNotes(long Id);
        Task<List<NotesModel>> TrashNotes(long UserId);
        Task<string> BackNotes(NotesModel notesModel);
        Task<string> AddImage(IFormFile file, long noteId);
        Task<string> SetReminder(long id, string reminder);
    }
    
    
}