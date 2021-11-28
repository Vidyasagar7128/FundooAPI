using FundoManager.Interfaces;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager _collaboratorManager;
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            _collaboratorManager = collaboratorManager;
        }
        [HttpPost]
        [Route("add")]
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
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("show")]
        public IActionResult ShowCollabs(long UserId)
        {
            try
            {
                List<NotesModel> result = _collaboratorManager.ShowCollab(UserId);
                if (result.Count >= 0)
                    return Ok(new { Status = true, Message = result });
                else
                    return BadRequest(new { Status = false, Message = "Something went Wrong!" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> CollabsDel(long UserId)
        {
            try
            {
                var result = await _collaboratorManager.DelCollab(UserId);
                if (result.Equals("Collaborator Deleted!"))
                    return Ok(new { Status = true, Message = result });
                else
                    return BadRequest(new { Status = false, Message = "Something went Wrong!" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
