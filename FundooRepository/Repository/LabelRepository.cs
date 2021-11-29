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
        /// Create Label
        /// </summary>
        /// <returns></returns>
        public async Task<string> CreateLabel(CreateLabelModel createLabel)
        {
            try
            {
                if (createLabel != null)
                {
                    var checkLabel = _userContext.LabelNames.Where(e => e.LabelName == createLabel.LabelName && e.UserId == createLabel.UserId).SingleOrDefault();
                    if (checkLabel != null)
                        return "Already Exist!";
                    else
                    {
                        _userContext.LabelNames.Add(createLabel);
                        await _userContext.SaveChangesAsync();
                        return "Label Created!";
                    }
                }
                else
                    return "Please Enter Labelname First!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Show All Labels
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<List<string>> labelList(long UserId)
        {
            try
            {
                return await _userContext.LabelNames.Where(e => e.UserId == UserId).Select(e => e.LabelName ).ToListAsync();
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public async Task<string> EditLabelName(EditLabelModel editLabelModel)
        {
            try
            {
                var checkLabelName = _userContext.LabelNames.Where(e => e.LabelName == editLabelModel.OldlabelName && e.UserId == editLabelModel.UserId).FirstOrDefault();
                if (checkLabelName != null)
                {
                    checkLabelName.LabelName = editLabelModel.NewLabelName;
                    _userContext.Entry(checkLabelName).State = EntityState.Modified;
                    await _userContext.SaveChangesAsync();
                    return "Label Edited!";
                }
                else
                    return "label does Not Exist!";
            }catch(ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteLabels(string LabelName, long UserId)
        {
            try
            {
                var checkLabelName = _userContext.LabelNames.Where(e => e.LabelName == LabelName && e.UserId == UserId).ToList();
                if (checkLabelName.Count() >= 1)
                {
                    _userContext.LabelNames.RemoveRange(checkLabelName);
                    await _userContext.SaveChangesAsync();
                    return "Label Deleted!";
                }
                else
                    return "You don't have Label!";
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> ShowlabelNotes(long UserId, string LabelNames)
        {
            try
            {
                var checkLabelList = (
                                from note in _userContext.Notes.ToList()
                                join label in _userContext.LabelNames.ToList()
                                on note.LabelId equals label.Id
                                where label.UserId == UserId
                                where label.LabelName == LabelNames
                                select new NotesModel()
                                {
                                    NoteId = note.NoteId,
                                    Title = note.Title,
                                    Body = note.Body,
                                    Theme = note.Theme,
                                    Image = note.Image,
                                    Reminder = note.Reminder,
                                    Pin = note.Pin,
                                    Status = note.Status,
                                    LabelId = note.LabelId,
                                    UserId = label.UserId,
                                }
                    ).ToList();
                if (checkLabelList.Count >= 1)
                {
                    return checkLabelList;
                }
                else
                    throw new Exception("You Don't have Notes in Label!");
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
