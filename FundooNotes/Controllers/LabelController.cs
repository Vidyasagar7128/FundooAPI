using FundoManager.Interfaces;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
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
    public class LabelController : Controller
    {
        private readonly ILabelManager _labelManager;
        public LabelController(ILabelManager labelManager)
        {
            _labelManager = labelManager;
        }
        /// <summary>
        /// Create Labels
        /// </summary>
        /// <param name="createLabelModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createlabel")]
        public async Task<IActionResult> LabelCreate([FromBody] CreateLabelModel createLabelModel)
        {
            try
            {
                var result = await _labelManager.CreateLabel(createLabelModel);
                if (result.Equals("Label Created!"))
                    return Ok(new { Status = true, Message = "Label Created!" });
                else
                    return BadRequest(new { Status = false, Message = "Already Exist!" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        /// <summary>
        /// Show Labels List
        /// </summary>
        /// <param name="ShowLabelModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("labellist")]
        public async Task<IActionResult> LabelShowList(long UserId)
        {
            try
            {
                var result = await _labelManager.ShowLabelList(UserId);
                if (result.Count >= 1)
                    return Ok(new { Status = true, Message = "Label List!", Data = result });
                else
                    return BadRequest(new { Status = false, Message = "You don't have Labels!" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        /// <summary>
        /// Edit Label Name
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="newLabelName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("editlabel")]
        public async Task<IActionResult> UpdateLabelName(EditLabelModel editLabelModel)
        {
            try
            {
                var result = await _labelManager.EditLabel(editLabelModel);
                if (result.Equals("Label Edited!"))
                    return Ok(new { Status = true, Message = result });
                else
                    return BadRequest(new { Status = false, Message = result });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("deletelabel")]
        public async Task<IActionResult> DeleteLabels(string LabelName, long UserId)
        {
            try
            {
                var result = await _labelManager.DeleteLabel(LabelName, UserId);
                if (result.Equals("Label Deleted!"))
                    return Ok(new { Status = true, Message = result });
                else
                    return BadRequest(new { Status = false, Message = result });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("data")]
        public IActionResult LabelsData(long UserId, string LabelName)
        {
            try
            {
                var result = _labelManager.ShowLabelLisData(UserId, LabelName);
                if (result.Count >= 1)
                    return Ok(new { Status = true, Message = "Label List!", Data = result });
                else
                    return BadRequest(new { Status = false, Message = result });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
