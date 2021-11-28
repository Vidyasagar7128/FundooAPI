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
                    if (_userContext.Notes.Where(e => e.NoteId == noteShareModel.NoteId && e.UserId == noteShareModel.SenderId) != null)
                    {
                        _userContext.Collaborators.AddRange(new CollaboratorModel() { Email = noteShareModel.Email, NoteId = noteShareModel.NoteId, SenderId = noteShareModel.SenderId });
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
    }
}
