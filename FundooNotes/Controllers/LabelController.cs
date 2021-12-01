// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    ///[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : Controller
    {
        private readonly ILabelManager _labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this._labelManager = labelManager;
        }

        /// <summary>
        /// Create Labels
        /// </summary>
        /// <param name="createLabelModel">passing CreateLabelModel</param>
        /// <returns></returns>
        [HttpPost]
        [Route("createlabel")]
        public async Task<IActionResult> LabelCreate([FromBody] CreateLabelModel createLabelModel)
        {
            try
            {
                var result = await this._labelManager.CreateLabel(createLabelModel);
                if (result.Equals("Label Created!"))
                {
                    return this.Ok(new { Status = true, Message = "Label Created!" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Already Exist!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// list of Labels
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <returns>list of Labels</returns>
        [HttpGet]
        [Route("labels")]
        public async Task<IActionResult> LabelShowList(long userId)
        {
            try
            {
                var result = await this._labelManager.ShowLabelList(userId);
                if (result.Count >= 1)
                {
                    return this.Ok(new { Status = true, Message = "Label List!", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "You don't have Labels!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Updating Label Name
        /// </summary>
        /// <param name="editLabelModel">passing EditLabelModel</param>
        /// <returns>label name updated or not</returns>
        [HttpPut]
        [Route("editlabel")]
        public async Task<IActionResult> UpdateLabelName(EditLabelModel editLabelModel)
        {
            try
            {
                var result = await this._labelManager.EditLabel(editLabelModel);
                if (result.Equals("Label Edited!"))
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
        /// Delete Labels
        /// </summary>
        /// <param name="labelName">passing labelName</param>
        /// <param name="userId">passing userId</param>
        /// <returns>label delete or not</returns>
        [HttpDelete]
        [Route("deletelabel")]
        public async Task<IActionResult> DeleteLabels(string labelName, long userId)
        {
            try
            {
                var result = await this._labelManager.DeleteLabel(labelName, userId);
                if (result.Equals("Label Deleted!"))
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
        /// Labels all notes Data
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="labelName">passing labelName</param>
        /// <returns>list of labels data notes</returns>
        [HttpGet]
        [Route("data")]
        public IActionResult LabelsData(long userId, string labelName)
        {
            try
            {
                var result = this._labelManager.ShowLabelLisData(userId, labelName);
                if (result.Count >= 1)
                {
                    return this.Ok(new { Status = true, Message = "Label List!", Data = result });
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
        [HttpDelete]
        [Route("delabel")]
        public async Task<IActionResult> DeleteLabel(long userId, string labelNames)
        {
            try
            {
                var result = await this._labelManager.DelLabel(userId, labelNames);
                if (result.Equals("Deleted!"))
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
    }
}
