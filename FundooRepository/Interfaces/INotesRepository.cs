using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface INotesRepository
    {
        IConfiguration Configuration { get; set; }

        Task<string> AddNote(NotesModel notes);
        Task<string> DeleteNote(long NoteId, long UserId);
        Task<List<NotesModel>> ShowNotes(long UserId);
        Task<string> UpdateNotes(NotesModel notesModel);
        Task<string> Color(NotesModel notesModel);
        Task<string> Archive(NotesModel notesModel);
        Task<string> Pin(NotesModel notesModel);
        Task<string> Trash(NotesModel notesModel);
        Task<List<NotesModel>> ShowArchiveNotes(long Id);
        Task<List<NotesModel>> ShowTrashNotes(long UserId);
        Task<string> RestoreNote(NotesModel notesModel);
        Task<string> UploadImg(IFormFile file, long userId);
    }
}