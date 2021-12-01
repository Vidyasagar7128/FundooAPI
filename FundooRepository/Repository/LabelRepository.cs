// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
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
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext _userContext;
        public LabelRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this._userContext = userContext;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Create Label
        /// </summary>
        /// <param name="createLabel">passing CreateLabelModel</param>
        /// <returns>created or not</returns>
        public async Task<string> CreateLabel(CreateLabelModel createLabel)
        {
            try
            {
                if (createLabel != null)
                {
                    var checkLabel = this._userContext.Labels.Where(e => e.LabelName == createLabel.LabelName && e.UserId == createLabel.UserId).SingleOrDefault();
                    if (checkLabel != null)
                    {
                        return "Already Exist!";
                    }
                    else
                    {
                        this._userContext.Labels.Add(createLabel);
                        await this._userContext.SaveChangesAsync();
                        return "Label Created!";
                    }
                }
                else
                {
                    return "Please Enter Labelname First!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Show All Labels
        /// </summary>
        /// <param name="userId">long userId</param>
        /// <returns>list of labels</returns>
        public async Task<List<string>> LabelList(long userId)
        {
            try
            {
                return await this._userContext.Labels.Where(e => e.UserId == userId).Select(e => e.LabelName).ToListAsync();
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        /// <summary>
        /// Edit LabelName
        /// </summary>
        /// <param name="editLabelModel">passing EditLabelModel</param>
        /// <returns>edited or not</returns>
        public async Task<string> EditLabelName(EditLabelModel editLabelModel)
        {
            try
            {
                var checkLabelName = this._userContext.Labels.Where(e => e.LabelName == editLabelModel.OldlabelName && e.UserId == editLabelModel.UserId).FirstOrDefault();
                if (checkLabelName != null)
                {
                    checkLabelName.LabelName = editLabelModel.NewLabelName;
                    this._userContext.Entry(checkLabelName).State = EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                    return "Label Edited!";
                }
                else
                {
                    return "label does Not Exist!";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete Labels
        /// </summary>
        /// <param name="labelName">labelName string</param>
        /// <param name="userId">UserId long</param>
        /// <returns>Deleted or not</returns>
        public async Task<string> DeleteLabels(string labelName, long userId)
        {
            try
            {
                var checkLabelName = this._userContext.Labels.Where(e => e.LabelName == labelName && e.UserId == userId).FirstOrDefault();
                if (checkLabelName != null)
                {
                    this._userContext.Labels.Remove(checkLabelName);
                    //this._userContext.Entry(checkLabelName).State = EntityState.Deleted;
                    await this._userContext.SaveChangesAsync();
                    return "Label Deleted!";
                }
                else
                {
                    return "You don't have Label!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// list of label Notes
        /// </summary>
        /// <param name="userId">long UserId</param>
        /// <param name="labelNames">string LabelName</param>
        /// <returns>list of label</returns>
        public List<NotesModel> ShowlabelNotes(long userId, string labelNames)
        {
            try
            {
                var checkLabelList = (
                                from note in this._userContext.Notes.ToList()
                                join label in this._userContext.Labels.ToList()
                                on note.LabelId equals label.Id
                                where label.UserId == userId
                                where label.LabelName == labelNames
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
                                }).ToList();
                if (checkLabelList.Count >= 1)
                {
                    return checkLabelList;
                }
                else
                {
                    throw new Exception("You Don't have Notes in Label!");
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> Delete(long userId, string labelNames)
        {
            try
            {
                var checkUser = this._userContext.Labels.Where(e => e.UserId == userId && e.LabelName == labelNames).FirstOrDefault();
                if (checkUser != null)
                {
                    this._userContext.Labels.Remove(checkUser);
                    this._userContext.Entry(checkUser).State = EntityState.Deleted;
                    await this._userContext.SaveChangesAsync();
                    return "Deleted!";
                }
                else
                {
                    return "User not Found!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
