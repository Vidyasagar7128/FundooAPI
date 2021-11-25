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
        private ICollaboratorManager _collaboratorManager;
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            _collaboratorManager = collaboratorManager;
        }
        [HttpPost]
        [Route("api/collaboratecreate")]
        public IActionResult CreateCb([FromQuery] long Id, [FromBody] List<string> Emails)
        {
            try
            {
                var result = _collaboratorManager.Collaborator(Id,Emails);
                if (result == "Collaborator Note Done!")
                    return Ok(new { Status = true, Message = result });
                else
                    return BadRequest(new { Status = false, Message = result });
            }
            catch(Exception e)
            {
                return Ok(new { Status = true, Message = e.Message });
            }
        }
    }
}
