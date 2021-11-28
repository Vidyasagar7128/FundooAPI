using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext _userContext;
        public CollaboratorRepository(IConfiguration configuration, UserContext userContext)
        {
            this.configuration = configuration;
            _userContext = userContext;
        }
        public IConfiguration configuration { get; set; }
        public async Task<string> Create(NoteShareModel noteShareModel)
        {
            try
            {
                var checkEmail = _userContext.Users.Where(e => e.Email == noteShareModel.Email).FirstOrDefault();
                if (checkEmail != null)
                {
                    if (_userContext.Notes.Where(e => e.NoteId == noteShareModel.NoteId && e.UserId == noteShareModel.SenderId).FirstOrDefault() != null)
                    {
                        _userContext.Collaborators.AddRange(new CollaboratorModel() { Email = noteShareModel.Email, NoteId = noteShareModel.NoteId, SenderId = noteShareModel.SenderId, ReceiverId = checkEmail.UserId });
                        await _userContext.SaveChangesAsync();
                        return "Note Shared!";
                    }
                    else
                        return "Something went Wrong!";
                }
                else
                    return "Failed to Share!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> ShowCollaborator(long UserId)
        {
            try
            {
                var checkShare = (
                                   from share in _userContext.Collaborators
                                   join note in _userContext.Notes
                                   on share.NoteId equals note.NoteId
                                   where share.ReceiverId == UserId
                                   select new NotesModel()
                                   {
                                       NoteId = note.NoteId,
                                       Title = note.Title,
                                       Body = note.Body,
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
                    throw new Exception("No notes have been sent to you!");
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteCollabs(long UserId)
        {
            try
            {
                var checkCollab = _userContext.Collaborators.Where(e => e.SenderId == UserId).FirstOrDefault();
                if (checkCollab != null)
                {
                    _userContext.Collaborators.Remove(checkCollab);
                    await _userContext.SaveChangesAsync();
                    return "Collaborator Deleted!";
                }
                else
                    return "Failed to Delete!";
            }
            catch(ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
