// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundoManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using FundooRepository.Interfaces;

    /// <summary>
    /// CollaboratorManager handles CollaboratorRepository
    /// </summary>
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// Creating ICollaboratorRepository's variable
        /// </summary>
        private readonly ICollaboratorRepository _collaboratorRepository;

        /// <summary>
        /// Initializes ICollaboratorRepository's variable
        /// </summary>
        /// <param name="collaboratorRepository">Passing ICollaboratorRepository</param>
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this._collaboratorRepository = collaboratorRepository;
        }
        /// <summary>
        /// Create collaborator
        /// </summary>
        /// <param name="noteShareModel">passing NoteShareModel</param>
        /// <returns>return string</returns>
        public async Task<string> Collaborator(NoteShareModel noteShareModel)
        {
            try
            {
                return await this._collaboratorRepository.Create(noteShareModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// show list of Collaborations
        /// </summary>
        /// <param name="userId">Passing userId</param>
        /// <returns>return List of Collaborations</returns>
        public List<NotesModel> ShowCollab(long userId)
        {
            try
            {
                return this._collaboratorRepository.ShowCollaborator(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// remove Collaboration
        /// </summary>
        /// <param name="userId">Passing userId</param>
        /// <returns>return string as done or not</returns>
        public async Task<string> DelCollab(long userId)
        {
            try
            {
                return await this._collaboratorRepository.DeleteCollabs(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
