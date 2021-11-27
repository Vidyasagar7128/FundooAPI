using FundoManager.Interfaces;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager _collaboratorManager;
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            _collaboratorManager = collaboratorManager;
        }
        [HttpPost]
        [Route("api/collaborator")]
        public async Task<IActionResult> CreateCb([FromBody] NoteShareModel noteShareModel)
        {
            try
            {
                var result = await _collaboratorManager.Collaborator(noteShareModel);
                if (result == "Note Shared!")
                    return Ok(new { Status = true, Message = result });
                else
                    return BadRequest(new { Status = false, Message = "Something went Wrong!" });
            }
            catch(Exception e)
            {
                return NotFound(new { Status = true, Message = e.Message });
            }
        }
    }
}
