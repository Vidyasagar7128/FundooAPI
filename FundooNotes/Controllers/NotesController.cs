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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// All routes of notes API
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        /// <summary>
        /// creating INotesManager variable _notesManager
        /// </summary>
        private readonly INotesManager _notesManager;

        /// <summary>
        /// assign values to _notesManager
        /// </summary>
        /// <param name="notesManager">passing parameter type of INotesManager</param>
        public NotesController(INotesManager notesManager)
        {
            this._notesManager = notesManager;
        }

        /// <summary>
        /// Adding Notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel from body</param>
        /// <returns>string result</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel notesModel)
        {
            try
            {
                var result = await this._notesManager.AddNewNote(notesModel);
                if (result.Equals("Note Added Succesfully!"))
                {
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
        /// Uploading Image
        /// </summary>
        /// <param name="file">IFormFile format</param>
        /// <param name="userId">passing userId</param>
        /// <returns>string result</returns>
        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> UploadImage(IFormFile file, long userId)
        {
            try
            {
                var result = await this._notesManager.AddImage(file, userId);
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
        /// showing All Notes
        /// </summary>
        /// <param name="UserId">using UserId</param>
        /// <returns>return List of notes</returns>
        [HttpGet]
        [Route("allnotes")]
        public async Task<IActionResult> AllNotes(long UserId)
        {
            try
            {
                List<NotesModel> result = await this._notesManager.ShowAllNotes(UserId);
                if (result != null)
                {
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
        /// Update Notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel</param>
        /// <returns>string result</returns>
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
        /// <param name="NoteId">passing NoteId</param>
        /// <param name="UserId">passing UserId</param>
        /// <returns>string result</returns>
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
        /// Restore Notes from trash
        /// </summary>
        /// <param name="notesModel">passing NotesModel from body</param>
        /// <returns>string result</returns>
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
        /// <param name="notesModel">passing NotesModel from body</param>
        /// <returns>string result</returns>
        [HttpPut]
        [Route("color")]
        public async Task<IActionResult> ChangeColors([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.ChangeColor(notesModel);
                if (color.Equals("Color Changed!"))
                {
                    return this.Ok(new { Status = true, Message = "Color Changed!" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to change Color!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Check Archive Status Put into Archive and Remove from it.
        /// </summary>
        /// <param name="notesModel">passing NotesModel from body</param>
        /// <returns>string result</returns>
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
        /// pin UnPin notes
        /// </summary>
        /// <param name="notesModel">passing NotesModel from body</param>
        /// <returns>string result</returns>
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

        /// <summary>
        /// notes move to Trash
        /// </summary>
        /// <param name="notesModel">passing NotesModel from body</param>
        /// <returns>string result</returns>
        [HttpPut]
        [Route("trash")]
        public async Task<IActionResult> TrashNotes([FromBody] NotesModel notesModel)
        {
            try
            {
                var result = await this._notesManager.TrashNote(notesModel);
                if (result.Equals("Moved to Trash!"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to Move Trash!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Show Archive
        /// </summary>
        /// <param name="id">passing UserId</param>
        /// <returns>list of Notes</returns>
        [HttpGet]
        [Route("archive")]
        public async Task<IActionResult> ShowArchive([FromQuery] long id)
        {
            try
            {
                List<NotesModel> result = await this._notesManager.ArchiveNotes(id);
                if (result.Count >= 1)
                {
                    return this.Ok(new { Status = true, Message = "Data is available", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Archive is Empty" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound( new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Show Trash list
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <returns>list of Notes </returns>
        [HttpGet]
        [Route("trashlist")]
        public async Task<IActionResult> ShowTrash(long userId)
        {
            try
            {
                List<NotesModel> result = await this._notesManager.TrashNotes(userId);
                if (result.Count >= 1)
                {
                    return this.Ok(new { Status = true, Message = "Trash Data", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Trash is Empty" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
