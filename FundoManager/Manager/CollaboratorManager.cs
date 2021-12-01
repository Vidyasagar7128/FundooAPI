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
        public List<NotesModel> ShowCollab(long UserId)
        {
            try
            {
                return _collaboratorRepository.ShowCollaborator(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DelCollab(long UserId)
        {
            try
            {
                return await _collaboratorRepository.DeleteCollabs(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
