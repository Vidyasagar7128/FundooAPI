using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FundooRepository.Interfaces
{
    public interface ICollaboratorRepository
    {
        IConfiguration configuration { get; set; }

        string Create(long Id, List<string> Emails);
    }
}