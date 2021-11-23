using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<List<NotesModel>> ShowNotes()
        {
            try
            {
                var notesLength = this._userContext.Notes.Count();
                if (notesLength >= 1)
                {
                    return await this._userContext.Notes.ToListAsync<NotesModel>();
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
        /// Update Notes
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        public async Task<string> UpdateNotes(NotesModel notesModel)
        {
            try
            {
                var idCheck =this._userContext.Notes.Where(e => e.NotesId == notesModel.NotesId).FirstOrDefault();
                if (idCheck != null)
                {
                    idCheck.Title = notesModel.Title;
                    idCheck.Body = notesModel.Body;
                    idCheck.Reminder = notesModel.Reminder;
                    idCheck.Theme = notesModel.Theme;
                    idCheck.Archive = notesModel.Archive;
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
        public async Task<string> DeleteNote(string Id)
        {
            try
            {
                var checkId = this._userContext.Notes.Where(e => e.NotesId.ToString().Equals(Id)).FirstOrDefault();
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
        public async Task<string> Color(NotesModel notesModel)
        {
            try
            {
                var checkColor = this._userContext.Notes.Where(e => e.NotesId == notesModel.NotesId).FirstOrDefault();
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
    }
}
