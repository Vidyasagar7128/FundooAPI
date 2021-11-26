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
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext _userContext;
        public LabelRepository(IConfiguration configuration, UserContext userContext)
        {
            Configuration = configuration;
            _userContext = userContext;
        }
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// Add to Labels
        /// </summary>
        /// <param name="labelStatusModel"></param>
        /// <returns></returns>
        public async Task<string> Add(LabelStatusModel labelStatusModel)
        {
            try
            {
                if (labelStatusModel.NoteId != 0 && labelStatusModel.UserId != 0)
                {
                    var checkNote = _userContext.Notes.Where(e => e.NoteId == labelStatusModel.NoteId && e.UserId == labelStatusModel.UserId).FirstOrDefault();
                    if (checkNote != null)
                    {
                        _userContext.Labels.Add(new LabelModel { Name = labelStatusModel.Name, NoteId = labelStatusModel.NoteId, UserId = labelStatusModel.UserId });
                        await _userContext.SaveChangesAsync();
                        return "Label Added sucessfully!";
                    }
                    else
                        return "Something went Wrong!";
                }
                else
                     return "you could not add Label on this Note!";
            }
            catch(ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Remove Labels
        /// </summary>
        /// <param name="labelStatusModel"></param>
        /// <returns></returns>
        public async Task<string> Remove(LabelStatusModel labelStatusModel)
        {
            try
            {
                var checkLabel = _userContext.Labels.Where(e => e.UserId == labelStatusModel.UserId && e.Name == labelStatusModel.Name).ToList();
                if(checkLabel.Count() >= 1)
                {
                    _userContext.Labels.RemoveRange(checkLabel);
                    await _userContext.SaveChangesAsync();
                    return "Label Deleted!";
                }else
                    return "something went wrong!";
            }
            catch(ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Show Label Names
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<List<string>> Show(long UserId)
        {
            try
            {
                if (_userContext.Labels.Where(e => e.UserId == UserId).Count() >= 1)
                {
                    return await _userContext.Labels.Where(e => e.UserId == UserId).Select(e => e.Name).Distinct().ToListAsync();
                }
                else
                    throw new Exception("You Don't have Labels!");
            }
            catch(ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> ShowLabelData(long UserId)
        {
            try
            {
                var labelData = (
                                 from note in _userContext.Notes.ToList()
                                 join label in _userContext.Labels.ToList() on note.NoteId equals label.NoteId
                                 where label.UserId == UserId///Session UserId
                                 select new NotesModel()
                                 {
                                     NoteId = note.NoteId,
                                     Title = note.Title,
                                     Body = note.Body,
                                     Theme = note.Theme,
                                     Reminder = note.Reminder,
                                     Pin = note.Pin,
                                     Status = note.Status,
                                     UserId = note.UserId
                                 }
                                 ).ToList();
                if (labelData != null)
                    return labelData;
                else
                    throw new Exception("Label is Empty!");
            }
            catch(ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
