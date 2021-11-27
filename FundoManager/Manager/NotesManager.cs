﻿using FundoManager.Interfaces;
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
        public async Task<string> AddImage(IFormFile file, long userId)
        {
            try
            {
                return await this._notesRepository.UploadImg(file,userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<NotesModel>> ShowAllNotes(long UserId)
        {
            try
            {
                return await this._notesRepository.ShowNotes(UserId);
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
        public async Task<string> DeleteNotes(long NoteId, long UserId)
        {
            try
            {
                return await this._notesRepository.DeleteNote(NoteId, UserId);
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
        public async Task<List<NotesModel>> TrashNotes(long UserId)
        {
            try
            {
                return await _notesRepository.ShowTrashNotes(UserId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
