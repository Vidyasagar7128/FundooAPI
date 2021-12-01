// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;


    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INotesManager _notesManager;
        private readonly ILogger<NotesController> _logger;
        public NotesController(INotesManager notesManager, ILogger<NotesController> logger)
        {
            this._notesManager = notesManager;
            this._logger = logger;
        }

        /// <summary>
        /// adding notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel model</param>
        /// <returns>return IActionResult</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel notesModel)
        {
            try
            {
                var result = await this._notesManager.AddNewNote(notesModel);
                if (result.Equals("Note Added Succesfully!"))
                {
                    _logger.LogInformation($"Notes Created by: {notesModel.Title} Title");
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = result,
                        Data = result
                    });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = result,
                    });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        /// <summary>
        /// Uploading Image to cloudinary
        /// </summary>
        /// <param name="file">IformFile type</param>
        /// <param name="userId">User Is</param>
        /// <returns></returns>
        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> UploadImage(IFormFile file, long userId)
        {
            try
            {
                var result = await _notesManager.AddImage(file,userId);
                if (result == "Image added Successfully")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// get all notes
        /// </summary>
        /// <param name="UserId">Passing userId</param>
        /// <returns>returns IActionResult</returns>
        [HttpGet]
        [Route("allnotes")]
        public async Task<IActionResult> AllNotes(long UserId)
        {
            try
            {
                List<NotesModel> result = await this._notesManager.ShowAllNotes(UserId);
                if (result != null)
                {
                    this._logger.LogInformation($"List of Notes!");
                    return this.Ok(new { Status = true, Message = "Data is available", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result.ToString(), Data = null});
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Update notes
        /// </summary>
        /// <param name="notesModel">Passing NotesModel</param>
        /// <returns>returns IActionResult</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateNote([FromBody] NotesModel notesModel)
        {
            try
            {
                var result = await this._notesManager.UpdateNote(notesModel);
                if (result.Equals("Note Update Succesfully!"))
                {
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = result.ToString(),
                        Data = result
                    });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = result,
                    });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Delete Notes
        /// </summary>
        /// <param name="NoteId">using NoteId</param>
        /// <param name="UserId">using UserId</param>
        /// <returns>return IActionResult</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteNote(long NoteId, long UserId)
        {
            try
            {
                var result = await this._notesManager.DeleteNotes(NoteId, UserId);
                if (result.Equals("Note Deleted Succesfully!"))
                {
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = result.ToString(),
                        Data = result.ToString()
                    });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = result,
                    });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Restore Notes
        /// </summary>
        /// <param name="notesModel">using NotesModel</param>
        /// <returns>IActionResult of RestoreNote</returns>
        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> RestoreNote([FromBody] NotesModel notesModel)
        {
            try
            {
                var result = await this._notesManager.BackNotes(notesModel);
                if (result.Equals("Note Restored Succesfully!"))
                {
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = "Note Restored Succesfully!",
                    });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = result,
                    });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Change Color of Note UI
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns>IActionResult of ChangeColors</returns>
        [HttpPut]
        [Route("color")]
        public async Task<IActionResult> ChangeColors([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.ChangeColor(notesModel);
                if(color.Equals("Color Changed!"))
                    return this.Ok(new { Status = true, Message = "Color Changed!"});
                else
                    return this.BadRequest(new { Status = false, Message = "Failed to change Color!" });
            }
            catch(Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Check Archive Status Put into Archive and Remove from it.
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns>IActionResult of ArchiveStatus</returns>
        [HttpPut]
        [Route("archive")]
        public async Task<IActionResult> ArchiveStatus([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.CheckArchive(notesModel);
                if (color.Equals("Archived!"))
                {
                    return this.Ok(new { Status = true, Message = "Archived!" });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Failed to Archive!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Pin notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>IActionResult of PinStatus</returns>
        [HttpPut]
        [Route("pin")]
        public async Task<IActionResult> PinStatus([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.CheckPin(notesModel);
                if (color.Equals("Pinned!"))
                {
                    return this.Ok(new { Status = true, Message = "Pinned!" });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Failed to Pin!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPut]
        [Route("trash")]
        public async Task<IActionResult> TrashNotes([FromBody] NotesModel notesModel)
        {
            try
            {
                var result = await _notesManager.TrashNote(notesModel);
                if (result.Equals("Moved to Trash!"))
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                else
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to Move Trash!" });
            }
            catch(Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("archive")]
        public async Task<IActionResult> ShowArchive([FromQuery] long Id)
        {
            try
            {
                List<NotesModel> result = await _notesManager.ArchiveNotes(Id);
                if (result.Count >= 1)
                    return Ok(new { Status = true, Message = "Data is available", Data = result });
                else
                    return BadRequest(new ResponseModel<string>() { Status = false, Message = "Archive is Empty" });
            }
            catch(Exception e)
            {
                return NotFound( new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("trashlist")]
        public async Task<IActionResult> ShowTrash(long UserId)
        {
            try
            {
                List<NotesModel> result = await _notesManager.TrashNotes(UserId);
                if(result.Count >= 1)
                    return Ok(new { Status = true, Message = "Trash Data", Data = result });
                else
                    return BadRequest(new { Status = false, Message = "Trash is Empty" });
            }
            catch(Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
