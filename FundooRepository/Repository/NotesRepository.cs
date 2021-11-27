using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext _userContext;
        public NotesRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this._userContext = userContext;
        }
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// Add notes
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
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
                    return "Failed!";
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// All Notes Show
        /// </summary>
        /// <returns></returns>
        public async Task<List<NotesModel>> ShowNotes(long UserId)
        {
            try
            {
                var notesLength = this._userContext.Notes.Where(e => e.UserId == UserId).ToList();
                if (notesLength.Count >= 1)
                {
                    return await this._userContext.Notes.Where(e => e.UserId == UserId).ToListAsync<NotesModel>();
                }
                else
                    throw new ArgumentNullException("You Don't have Notes!");
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <returns></returns>
        public async Task<string> UploadImg(IFormFile file,long userId)
        {
            try
            {
                if (file != null)
                {
                    var checkNotes = _userContext.Notes.Where(e => e.UserId == userId).FirstOrDefault();
                    Cloudinary cloudinary = new Cloudinary(new Account(
                                                "171559438548485",
                                                "Cw3WujFZNaBxKYc0K0pj3dhKExg",
                                                "dwpsmsxy6"));
                    var uploadImage = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };

                    var uploadResult = cloudinary.Upload(uploadImage);
                    var uploadPath = uploadResult.Url;
                    checkNotes.Image = uploadPath.ToString();
                    this._userContext.Notes.Update(checkNotes);
                    await this._userContext.SaveChangesAsync();
                    return "Image added Successfully";
                }
                else
                    return "Something went Wrong!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Update Notes
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        public async Task<string> UpdateNotes(NotesModel notesModel)
        {
            try
            {
                var idCheck =this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if (idCheck != null)
                {
                    idCheck.Title = notesModel.Title;
                    idCheck.Body = notesModel.Body;
                    idCheck.Reminder = notesModel.Reminder;
                    idCheck.Theme = notesModel.Theme;
                    idCheck.Status = notesModel.Status;
                    idCheck.Pin = notesModel.Pin;

                    this._userContext.Entry<NotesModel>(idCheck).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Note Update Succesfully!";
                }
                else
                    return "Failed to Update!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Delete Permenantly
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<string> DeleteNote(long NoteId, long UserId)
        {
            try
            {
                var checkId = this._userContext.Notes.Where(e => e.NoteId == NoteId && e.UserId == UserId).FirstOrDefault();
                if (checkId != null)
                {
                    this._userContext.Notes.Remove(checkId);
                    await this._userContext.SaveChangesAsync();
                    return "Note Deleted Succesfully!";
                }
                else
                    return "Failed to Delete!";
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Restore from Trash 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<string> RestoreNote(NotesModel notesModel)
        {
            try
            {
                var checkId = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if (checkId != null)
                {
                    checkId.Status = checkId.Status == 2 ? 0 : 2;
                    checkId.Pin = checkId.Pin == true ? false : false;
                    _userContext.Entry(checkId).State = EntityState.Modified;
                    await _userContext.SaveChangesAsync();
                    return "Note Restored Succesfully!";
                }
                else
                    return "Failed to Restore!";
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> Color(NotesModel notesModel)
        {
            try
            {
                var checkColor = this._userContext.Notes.Where(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if (checkColor != null)
                {
                    checkColor.Theme = notesModel.Theme;
                    this._userContext.Entry<NotesModel>(checkColor).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Color Changed!";
                } else
                    return "Failed to change Color!";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> Archive(NotesModel notesModel)
        {
            try
            {
                var checkArchive = this._userContext.Notes.Where<NotesModel>(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if(checkArchive != null)
                {
                    checkArchive.Status = checkArchive.Status == 0 ? 1 : 0;
                    checkArchive.Pin = checkArchive.Pin == true ? false : false;
                    this._userContext.Entry<NotesModel>(checkArchive).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Archived!";
                }
                else
                    return "Failed to Archive!";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Pin or Unpin from Task
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        public async Task<string> Pin(NotesModel notesModel)
        {
            try
            {
                var checkPin = this._userContext.Notes.Where<NotesModel>(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if (checkPin != null)
                {
                    checkPin.Pin = checkPin.Pin == false ? true : false;
                    checkPin.Status = checkPin.Status == 1 ? 0 : 0;
                    this._userContext.Entry<NotesModel>(checkPin).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Pinned!";
                }
                else
                    return "Failed to Pin!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Move to Trash Notes
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        public async Task<string> Trash(NotesModel notesModel)
        {
            try
            {
                var checkStatus = _userContext.Notes.Where(e => e.NoteId == notesModel.NoteId).FirstOrDefault();
                if(checkStatus != null)
                {
                    checkStatus.Status = checkStatus.Status == 2 ? 0 : 2;
                    checkStatus.Pin = false;
                    _userContext.Entry(checkStatus).State = EntityState.Modified;
                    await _userContext.SaveChangesAsync();
                    return "Moved to Trash!";
                }else
                    return "Failed to Move Trash!";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Show Archive Notes
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        public async Task<List<NotesModel>> ShowArchiveNotes(long Id)
        {
            try
            {
                var checkStatus = _userContext.Notes.Where(e => e.UserId == Id).Count();
                if(checkStatus >= 1)
                    return await _userContext.Notes.Where(e => e.Status == 1).ToListAsync();
                 else
                    throw new ArgumentNullException("No Archive Notes!");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Trash Notes
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<NotesModel>> ShowTrashNotes(long Id)
        {
            try
            {
                var checkStatus = _userContext.Notes.Where(e => e.UserId == Id).Count();
                if (checkStatus >= 1)
                    return await _userContext.Notes.Where(e => e.Status == 2).ToListAsync();
                else
                    throw new ArgumentNullException("Trash is Empty!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
///Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InNvbnVAZ21haWwuY29tIiwibmJmIjoxNjM3OTI4MTA0LCJleHAiOjE2Mzc5Mjk5MDQsImlhdCI6MTYzNzkyODEwNH0.MsCuRtXz6caFoi7o7xo_wOygXDnXLmzMtZDnGvD6l54
