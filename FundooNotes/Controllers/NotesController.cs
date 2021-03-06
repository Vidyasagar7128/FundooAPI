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
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// NotesController handles the all routes of notes API
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
        private StringValues head;

        /// <summary>
        /// create ILogger variable
        /// </summary>
        private readonly ILogger<NotesController> _logger;

        /// <summary>
        /// assign values to private variables
        /// </summary>
        /// <param name="notesManager">passing INotesManager as variable</param>
        /// <param name="logger">passing ILogger INotesManager as variable</param>
        public NotesController(INotesManager notesManager, ILogger<NotesController> logger)
        {
            this._notesManager = notesManager;
            this._logger = logger;
        }

        /// <summary>
        /// For Header
        /// </summary>
        /// <returns></returns>

        //public string GetHeaderValues()
        //{
        //    try
        //    {
        //        StringValues head;
        //        Request.Headers.TryGetValue("Authorization", out head);
        //        return head;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

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
                Request.Headers.TryGetValue("Authorization", out this.head);
                var result = await this._notesManager.AddNewNote(this.head, notesModel);
                if (result.Equals("Note Added Succesfully!"))
                {
                    this._logger.LogInformation($"Notes Created by: {notesModel.Title} Title");
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
        /// <param name="noteId">User Is</param>
        /// <returns></returns>
        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> UploadImage(IFormFile file, long noteId)
        {
            try
            {
                var result = await this._notesManager.AddImage(file, noteId);
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
        public async Task<IActionResult> AllNotes()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                List<NotesModel> result = await this._notesManager.ShowAllNotes(this.head);
                if (result.Count >= 1)
                {
                    this._logger.LogInformation($"List of Notes!");
                    return this.Ok(new { Status = true, Message = "Notes fetched succesfully.", Data = result });
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
        /// <param name="notesModel">passing NotesModel through body</param>
        /// <returns>IActionResult of ChangeColors</returns>
        [HttpPut]
        [Route("color")]
        public async Task<IActionResult> ChangeColors([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.ChangeColor(notesModel);
                if (color.Equals("Color Changed!"))
                    return this.Ok(new { Status = true, Message = "Color Changed!"});
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
        /// <param name="notesModel">Passing NotesModel</param>
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

        /// <summary>
        /// to Set Reminder
        /// </summary>
        /// <param name="id">Passing id</param>
        /// <param name="reminder">Passing Reminder</param>
        /// <returns>return string</returns>
        [HttpPut]
        [Route("reminder")]
        public async Task<IActionResult> Reminder(long id, string reminder)
        {
            try
            {
                var result = await this._notesManager.SetReminder(id, reminder);
                if (reminder.Equals("Reminder Set!"))
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


        //[HttpGet]
        //[Route("token")]
        //public IActionResult Tokens()
        //{
        //    try
        //    {
        //        Request.Headers.TryGetValue("Authorization", out this.head);
        //        var result = this._notesManager.GetToken(this.head);
        //        if (result != null)
        //        {
        //            return this.Ok(new { Status = true, Message = "Token Data", Data = result });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { Status = false, Message = "Token is Empty" });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return this.NotFound(new { Status = false, Message = e.Message });
        //    }
        //}
    }
}
