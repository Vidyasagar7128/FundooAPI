using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface ICollaboratorManager
    {
        Task<string> Collaborator(NoteShareModel noteShareModel);
    }
}