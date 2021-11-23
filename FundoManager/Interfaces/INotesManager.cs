using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface INotesManager
    {
        Task<string> AddNewNote(NotesModel notesModel);
        Task<string> DeleteNotes(string Id);
        Task<List<NotesModel>> ShowAllNotes();
        Task<string> UpdateNote(NotesModel notesModel);
    }
}