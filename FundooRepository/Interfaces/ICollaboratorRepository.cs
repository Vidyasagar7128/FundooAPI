using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<string> Create(NoteShareModel noteShareModel);
        List<NotesModel> ShowCollaborator(long UserId);
        Task<string> DeleteCollabs(long UserId);
    }
}