using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface INotesRepository
    {
        IConfiguration Configuration { get; set; }

        Task<string> AddNote(NotesModel notes);
        Task<string> DeleteNote(string Id);
        Task<List<NotesModel>> ShowNotes();
        Task<string> UpdateNotes(NotesModel notesModel);
        Task<string> Color(NotesModel notesModel);
        Task<string> Archive(NotesModel notesModel);
    }
}