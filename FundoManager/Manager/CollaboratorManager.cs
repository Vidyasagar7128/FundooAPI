using FundoManager.Interfaces;
using FundooModels;
using FundooRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundoManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private ICollaboratorRepository _collaboratorRepository;
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }
        public string Collaborator(long Id, List<string> Emails)
        {
            try
            {
                return _collaboratorRepository.Create(Id, Emails);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
