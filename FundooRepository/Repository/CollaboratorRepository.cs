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
        public string Create(long Id, List<string> Emails)
        {
            try
            {
                foreach (var email in Emails)
                {
                    var checkEmail = _userContext.Users.Where(e => e.Email == email).Select(x => x.Email).FirstOrDefault();
                    if (checkEmail != null)
                    {
                        return "Collaborator Note Done!";
                    }
                    else
                        return "Empty Collaborator!";
                }
                return "Collaborator Note Failed!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
