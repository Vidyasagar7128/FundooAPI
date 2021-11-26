using FundoManager.Interfaces;
using FundooModels;
using FundooRepository.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public string AddImage(IFormFile file)
        {
            try
            {
                return this._notesRepository.UploadImg(file);
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
        public async Task<string> BackNotes(NotesModel notesModel)
        {
            try
            {
                return await _notesRepository.RestoreNote(notesModel);
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
        public async Task<string> CheckArchive(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.Archive(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> CheckPin(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.Pin(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> TrashNote(NotesModel notesModel)
        {
            try
            {
                return await _notesRepository.Trash(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<NotesModel>> ArchiveNotes(long Id)
        {
            try
            {
                return await _notesRepository.ShowArchiveNotes(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<NotesModel>> TrashNotes(long Id)
        {
            try
            {
                return await _notesRepository.ShowTrashNotes(Id);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
