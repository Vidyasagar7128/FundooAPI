// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// NotesRepository it handles the notes API
    /// </summary>
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// UserContext's variable declared
        /// </summary>
        private readonly UserContext _userContext;

        /// <summary>
        /// Constructor for class
        /// </summary>
        /// <param name="configuration">passing configuration</param>
        /// <param name="userContext">passing userContext</param>
        public NotesRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this._userContext = userContext;
        }

        /// <summary>
        /// getter method for Configuring dependencies
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Add notes
        /// </summary>
        /// <param name="notes">passing NotesModel</param>
        /// <returns>added or not </returns>
        /// <returns>string as note added or not</returns>
        public async Task<string> AddNote(NotesModel notes)
        {
            try
            {
                if (notes != null)
                {
                    this._userContext.Notes.Add(notes);
                    await this._userContext.SaveChangesAsync();
                    return "Note Added Succesfully!";
                }
                else
                {
                    return "Failed!";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// show all notes
        /// </summary>
        /// <param name="userId">long UserId</param>
        /// <returns>list of NotesModel</returns>
        public async Task<List<NotesModel>> ShowNotes(long userId)
        {
            try
            {
                var notesLength = this._userContext.Notes.Where(e => e.UserId == userId).ToList();
                if (notesLength.Count >= 1)
                {
                    return await this._userContext.Notes.Where(e => e.UserId == userId).ToListAsync<NotesModel>();
                }
                else
                {
                    throw new ArgumentNullException("You Don't have Notes!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Uploading Image
        /// </summary>
        /// <param name="file">IFormFile type</param>
        /// <param name="userId">User Id</param>
        /// <returns>Uploaded or not</returns>
        public async Task<string> UploadImg(IFormFile file, long userId)
        {
            try
            {
                var checkNote = this._userContext.Notes.Where(e => e.UserId == userId).FirstOrDefault();
                if (checkNote != null)
                {
                    Cloudinary cloudinary = new Cloudinary(new Account(
                                                    "dwpsmsxy6",
                                                    "171559438548485",
                                                    "Cw3WujFZNaBxKYc0K0pj3dhKExg"));
                    var uploadImage = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadImage);
                    var uploadPath = uploadResult.Url;
                    checkNote.Image = uploadPath.ToString();
                    this._userContext.Entry(checkNote).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Image added Successfully";
                }
                else
                {
                    return "Something went Wrong!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update Note
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>Update or not</returns>

        public async Task<string> UpdateNotes(NotesModel notesModel)
        {
            try
            {
                var idCheck = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId && e.UserId == notesModel.UserId).FirstOrDefault();
                if (idCheck != null)
                {
                    idCheck.Title = notesModel.Title;
                    idCheck.Body = notesModel.Body;
                    idCheck.Reminder = notesModel.Reminder;
                    idCheck.Theme = notesModel.Theme;
                    idCheck.Status = notesModel.Status;
                    idCheck.Pin = notesModel.Pin;

                    this._userContext.Entry(idCheck).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Note Update Succesfully!";
                }
                else
                {
                    return "Failed to Update!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete Note
        /// </summary>
        /// <param name="noteId">passing NoteId</param>
        /// <param name="userId">and userId</param>
        /// <returns>deleted or not</returns>
        public async Task<string> DeleteNote(long noteId, long userId)
        {
            try
            {
                var checkId = this._userContext.Notes.Where(e => e.NoteId == noteId && e.UserId == userId).FirstOrDefault();
                if (checkId != null)
                {
                    this._userContext.Notes.Remove(checkId);
                    await this._userContext.SaveChangesAsync();
                    return "Note Deleted Succesfully!";
                }
                else
                {
                    return "Failed to Delete!";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Restore Note
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>restored or not</returns>
        public async Task<string> RestoreNote(NotesModel notesModel)
        {
            try
            {
                var checkId = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if (checkId != null)
                {
                    checkId.Status = checkId.Status == 2 ? 0 : 2;
                    checkId.Pin = checkId.Pin == true ? false : false;
                    this._userContext.Entry(checkId).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Note Restored Succesfully!";
                }
                else
                {
                    return "Failed to Restore!";
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// changing Color
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>color changed or not</returns>
        public async Task<string> Color(NotesModel notesModel)
        {
            try
            {
                var checkColor = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId && e.UserId == notesModel.UserId).FirstOrDefault();
                if (checkColor != null)
                {
                    checkColor.Theme = notesModel.Theme;
                    this._userContext.Entry(checkColor).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Color Changed!";
                } 
                else
                {
                    return "Failed to change Color!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// note Archive
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>done or not</returns>
        public async Task<string> Archive(NotesModel notesModel)
        {
            try
            {
                var checkArchive = this._userContext.Notes.Where<NotesModel>(e => e.NoteId == notesModel.NoteId && e.UserId == notesModel.UserId).FirstOrDefault();
                if (checkArchive != null)
                {
                    checkArchive.Status = checkArchive.Status == 0 ? 1 : 0;
                    checkArchive.Pin = checkArchive.Pin == true ? false : false;
                    this._userContext.Entry(checkArchive).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Archived!";
                }
                else
                {
                    return "Failed to Archive!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Pin or Unpin from Task
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>pin or not</returns>
        public async Task<string> Pin(NotesModel notesModel)
        {
            try
            {
                var checkPin = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId && e.UserId == notesModel.UserId).FirstOrDefault();
                if (checkPin != null)
                {
                    checkPin.Pin = checkPin.Pin == false ? true : false;
                    checkPin.Status = checkPin.Status == 1 ? 0 : 0;
                    this._userContext.Entry(checkPin).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Pinned!";
                }
                else
                {
                    return "Failed to Pin!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Move to Trash Notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>trashed or not</returns>
        public async Task<string> Trash(NotesModel notesModel)
        {
            try
            {
                var checkStatus = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId && e.UserId == notesModel.UserId).FirstOrDefault();
                if (checkStatus != null)
                {
                        checkStatus.Status = checkStatus.Status == 2 ? 0 : 2;
                        checkStatus.Pin = false;
                        this._userContext.Entry(checkStatus).State = EntityState.Modified;
                        await this._userContext.SaveChangesAsync();
                        return "Moved to Trash!";
                }
                else
                {
                    return "Failed to Move Trash!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Show Archive Notes
        /// </summary>
        /// <param name="Id">Passing ID</param>
        /// <returns>list of notes archived</returns>
        public async Task<List<NotesModel>> ShowArchiveNotes(long Id)
        {
            try
            {
                var checkStatus = this._userContext.Notes.Where(e => e.UserId == Id).Count();
                if (checkStatus >= 1)
                {
                    return await this._userContext.Notes.Where(e => e.Status == 1 && e.UserId == Id).ToListAsync();
                }
                 else
                {
                    throw new ArgumentNullException("No Archive Notes!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// show Trash Notes
        /// </summary>
        /// <param name="UserId">passing UserId</param>
        /// <returns>List of trashed list</returns>
        public async Task<List<NotesModel>> ShowTrashNotes(long UserId)
        {
            try
            {
                var checkStatus = this._userContext.Notes.Where(e => e.UserId == UserId).Count();
                if (checkStatus >= 1)
                {
                    return await this._userContext.Notes.Where(e => e.Status == 2 && e.UserId == UserId).ToListAsync();
                }
                else
                {
                    throw new ArgumentNullException("Trash is Empty!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
