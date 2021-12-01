// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundoManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using FundooRepository.Interfaces;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// NotesManager manage the Notes repo
    /// </summary>
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// created variable of INotesRepository
        /// </summary>
        private readonly INotesRepository _notesRepository;

        /// <summary>
        /// assign values to the _notesRepository
        /// </summary>
        /// <param name="notesRepository">Interface INotesRepository</param>
        /// 
        public NotesManager(INotesRepository notesRepository)
        {
            this._notesRepository = notesRepository;
        }

        /// <summary>
        /// Add note
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>done or not</returns>
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

        /// <summary>
        /// upload Image
        /// </summary>
        /// <param name="file">passing IFormFile format input</param>
        /// <param name="userId">passing Id</param>
        /// <returns>uploaded or not as string message</returns>
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

        /// <summary>
        /// List of Notes
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>notes List</returns>
        public async Task<List<NotesModel>> ShowAllNotes(long userId)
        {
            try
            {
                return await this._notesRepository.ShowNotes(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Updating notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>updated or failed</returns>
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

        /// <summary>
        /// Delete Notes
        /// </summary>
        /// <param name="noteId">Note Id</param>
        /// <param name="userId">User Id</param>
        /// <returns>deleted or not</returns>
        public async Task<string> DeleteNotes(long noteId, long userId)
        {
            try
            {
                return await this._notesRepository.DeleteNote(noteId, userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// restore note from trash
        /// </summary>
        /// <param name="notesModel">Notes Model</param>
        /// <returns>restored notes</returns>
        public async Task<string> BackNotes(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.RestoreNote(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// change color
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>color changed or not</returns>
        public async Task<string> ChangeColor(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.Color(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Check Archive
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>archive or not</returns>
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

        /// <summary>
        /// Check Pin
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>note pinned or not</returns>
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

        /// <summary>
        /// move to Trash and back Note
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>move to Trash and back Note</returns>
        public async Task<string> TrashNote(NotesModel notesModel)
        {
            try
            {
                return await this._notesRepository.Trash(notesModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// show Archive Notes
        /// </summary>
        /// <param name="id">passing UserId</param>
        /// <returns>List of archived notes</returns>
        public async Task<List<NotesModel>> ArchiveNotes(long id)
        {
            try
            {
                return await this._notesRepository.ShowArchiveNotes(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// list of trash notes
        /// </summary>
        /// <param name="userId">passing UserId</param>
        /// <returns>list of trash notes</returns>
        public async Task<List<NotesModel>> TrashNotes(long userId)
        {
            try
            {
                return await this._notesRepository.ShowTrashNotes(userId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
