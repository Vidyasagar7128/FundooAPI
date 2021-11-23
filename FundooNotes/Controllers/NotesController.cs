using FundoManager.Interfaces;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesManager _notesManager;
        public NotesController(INotesManager notesManager)
        {
            this._notesManager = notesManager;
        }
        [HttpPost]
        [Route("api/addnote")]
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
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("api/allnotes")]
        public async Task<IActionResult> AllNotes()
        {
            try
            {
                List<NotesModel> result = await this._notesManager.ShowAllNotes();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Data is available", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result.ToString(), Data = null});
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("api/update")]
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
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        [Route("api/delete")]
        public async Task<IActionResult> DeleteNote([FromQuery] string Id)
        {
            try
            {
                var result = await this._notesManager.DeleteNotes(Id);
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
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Change Color of Note UI
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/color")]
        public async Task<IActionResult> ChangeColors([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.ChangeColor(notesModel);
                if(color.Equals("Color Changed!"))
                    return this.Ok(new { Status = true,Message = "Color Changed!"});
                else
                    return this.BadRequest(new { Status = true, Message = "Color Changed!" });
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Check Archive Status Put into Archive and Remove from it.
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/archive")]
        public async Task<IActionResult> ArchiveStatus([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.CheckArchive(notesModel);
                if (color.Equals("Archived!"))
                    return this.Ok(new { Status = true, Message = "Archived!" });
                else
                    return this.BadRequest(new { Status = true, Message = "Failed to Archive!" });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("api/pin")]
        public async Task<IActionResult> PinStatus([FromBody] NotesModel notesModel)
        {
            try
            {
                var color = await this._notesManager.CheckPin(notesModel);
                if (color.Equals("Pinned!"))
                    return this.Ok(new { Status = true, Message = "Pinned!" });
                else
                    return this.BadRequest(new { Status = true, Message = "Failed to Pin!" });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
