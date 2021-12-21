using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface INotesRepository
    {
        Task<string> AddNote(StringValues token, NotesModel notes);
        Task<string> DeleteNote(long NoteId, long UserId);
        Task<List<NotesModel>> ShowNotes(StringValues token);
        Task<string> UpdateNotes(NotesModel notesModel);
        Task<string> Color(NotesModel notesModel);
        Task<string> Archive(NotesModel notesModel);
        Task<string> Pin(NotesModel notesModel);
        Task<string> Trash(NotesModel notesModel);
        Task<List<NotesModel>> ShowArchiveNotes(long Id);
        Task<List<NotesModel>> ShowTrashNotes(long UserId);
        Task<string> RestoreNote(NotesModel notesModel);
        Task<string> UploadImg(IFormFile file, long noteId);
        Task<string> Reminder(long id, string reminder);

        //JwtSecurityToken ValidateJwtToken(StringValues token);
    }
}