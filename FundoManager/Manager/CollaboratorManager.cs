using FundoManager.Interfaces;
using FundooModels;
using FundooRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundoManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }
        public async Task<string> Collaborator(NoteShareModel noteShareModel)
        {
            try
            {
                return await _collaboratorRepository.Create(noteShareModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
