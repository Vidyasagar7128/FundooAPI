// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="Bridgelabz">
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
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CollaboratorRepository handles operations on Collaborations
    /// </summary>
    public class CollaboratorRepository : ICollaboratorRepository
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
        public CollaboratorRepository(IConfiguration configuration, UserContext userContext)
        {
            this.configuration = configuration;
            this._userContext = userContext;
        }

        /// <summary>
        /// getter method for Configuring dependencies
        /// </summary>
        public IConfiguration configuration { get; set; }

        /// <summary>
        /// Create collaborator
        /// </summary>
        /// <param name="noteShareModel">passing NoteShareModel</param>
        /// <returns>return string</returns>
        public async Task<string> Create(NoteShareModel noteShareModel)
        {
            try
            {
                var checkEmail = this._userContext.Users.Where(e => e.Email == noteShareModel.Email).FirstOrDefault();
                if (checkEmail != null)
                {
                    if (this._userContext.Notes.Where(e => e.NoteId == noteShareModel.NoteId && e.UserId == noteShareModel.SenderId).FirstOrDefault() != null)
                    {
                        this._userContext.Collaborators.AddRange(new CollaboratorModel() { Email = noteShareModel.Email, NoteId = noteShareModel.NoteId, SenderId = noteShareModel.SenderId, ReceiverId = checkEmail.UserId });
                        await this._userContext.SaveChangesAsync();
                        return "Note Shared!";
                    }
                    else
                    {
                        return "Something went Wrong!";
                    }
                }
                else
                {
                    return "Failed to Share!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// show list of Collaborations
        /// </summary>
        /// <param name="UserId">Passing UserId</param>
        /// <returns>return List of Collaborations</returns>
        public List<NotesModel> ShowCollaborator(long UserId)
        {
            try
            {
                var checkShare = (
                                   from share in this._userContext.Collaborators
                                   join note in this._userContext.Notes
                                   on share.NoteId equals note.NoteId
                                   where share.ReceiverId == UserId
                                   select new NotesModel()
                                   {
                                       NoteId = note.NoteId,
                                       Title = note.Title,
                                       Body = note.Body,
                                       Image = note.Image,
                                       Theme = note.Theme,
                                       Reminder = note.Reminder,
                                       Status = note.Status,
                                       Pin = note.Pin,
                                       UserId = note.UserId
                                   }
                                  ).ToList();
                if (checkShare.Count >= 1)
                {
                    return checkShare;
                }
                else
                {
                    throw new Exception("No notes have been sent to you!");
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// remove Collaboration
        /// </summary>
        /// <param name="userId">Passing userId</param>
        /// <returns>return string as done or not</returns>
        public async Task<string> DeleteCollabs(long userId)
        {
            try
            {
                var checkCollab = this._userContext.Collaborators.Where(e => e.SenderId == userId).FirstOrDefault();
                if (checkCollab != null)
                {
                    this._userContext.Collaborators.Remove(checkCollab);
                    await this._userContext.SaveChangesAsync();
                    return "Collaborator Deleted!";
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
    }
}
