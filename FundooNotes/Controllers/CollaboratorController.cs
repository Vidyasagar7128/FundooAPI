// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CollaboratorController is responsible for Collaborator API
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : Controller
    {
        /// <summary>
        /// Created variable of ICollaboratorManager Interface
        /// </summary>
        private readonly ICollaboratorManager _collaboratorManager;

        /// <summary>
        /// Initializes a new instance of the ICollaboratorManager
        /// </summary>
        /// <param name="collaboratorManager">passing ICollaboratorManager Interface</param>
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this._collaboratorManager = collaboratorManager;
        }

        /// <summary>
        /// Creating Collaboration
        /// </summary>
        /// <param name="noteShareModel">Passing NoteShareModel</param>
        /// <returns>return done or not as string</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCb([FromBody] NoteShareModel noteShareModel)
        {
            try
            {
                var result = await this._collaboratorManager.Collaborator(noteShareModel);
                if (result == "Note Shared!")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// show list of Collaborations
        /// </summary>
        /// <param name="userId">Passing userId</param>
        /// <returns>return List of Collaborations</returns>
        [HttpGet]
        [Route("show")]
        public IActionResult ShowCollabs(long userId)
        {
            try
            {
                List<NotesModel> result = this._collaboratorManager.ShowCollab(userId);
                if (result.Count >= 0)
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// remove Collaboration
        /// </summary>
        /// <param name="userId">Passing userId</param>
        /// <returns>return string as done or not</returns>
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> CollabsDel(long userId)
        {
            try
            {
                var result = await this._collaboratorManager.DelCollab(userId);
                if (result.Equals("Collaborator Deleted!"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
