using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface INotesManager
    {
        Task<string> AddNewNote(StringValues token,NotesModel notesModel);
        Task<string> DeleteNotes(long NoteId, long UserId);
        Task<List<NotesModel>> ShowAllNotes(StringValues token);
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

        //JwtSecurityToken GetToken(StringValues token);
    }
    
    
}