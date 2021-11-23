using FundoManager.Interfaces;
using FundooModels;
using FundooRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundoManager.Manager
{
    public class NotesManager : INotesManager
    {
        private INotesRepository _notesRepository;
        public NotesManager(INotesRepository notesRepository)
        {
            this._notesRepository = notesRepository;
        }
        public async Task<string> AddNewNote(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.AddNote(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<NotesModel>> ShowAllNotes()
        {
            try
            {
                return await this._notesRepository.ShowNotes();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateNote(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.UpdateNotes(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteNotes(string Id)
        {
            try
            {
                return await this._notesRepository.DeleteNote(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> ChangeColor(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.Color(notesModel);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
