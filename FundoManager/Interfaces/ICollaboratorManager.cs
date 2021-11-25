using FundooModels;
using System.Collections.Generic;

namespace FundoManager.Interfaces
{
    public interface ICollaboratorManager
    {
        string Collaborator(long Id, List<string> Emails);
    }
}