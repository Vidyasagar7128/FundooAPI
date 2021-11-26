using FundoManager.Interfaces;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class LabelController : Controller
    {
        private ILabelManager _labelManager;
        public LabelController(ILabelManager labelManager)
        {
            _labelManager = labelManager;
        }
        [HttpPost]
        [Route("api/label")]
        public async Task<IActionResult> Add([FromBody] LabelStatusModel labelStatusModel)
        {
            try
            {
                var result = await _labelManager.AddLabel(labelStatusModel);
                if (result == "Label Added sucessfully!")
                    return Ok(new { Status = true, Message = "Label Added sucessfully!" });
                else
                    return BadRequest(new { Status = false, Message = "Something went Wrong!" });
            }
            catch(Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("api/label")]
        public async Task<IActionResult> DeleteLabel([FromBody] LabelStatusModel labelStatusModel)
        {
            try
            {
                var result = await _labelManager.RemoveLabel(labelStatusModel);
                if (result == "Label Deleted!")
                    return Ok(new ResponseModel<string> { Status = true, Message = result });
                else
                    return BadRequest(new ResponseModel<string> { Status = false, Message = result });
            }
            catch(Exception e)
            {
                return NotFound(new ResponseModel<string> { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("api/labels")]
        public async Task<IActionResult> LabelsList([FromBody] long UserId)
        {
            try
            {
                var result = await _labelManager.ShowLabels(UserId);
                if (!result.Contains("You Don't have Labels!"))
                    return Ok(new { Status = true, Message = "Labels!",Data = result });
                else
                    return BadRequest(new { Status = false, Message = "Something went Wrong!" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPost]
        [Route("api/labeldata")]
        public IActionResult LabelsDataList([FromBody] int UserId)
        {
            try
            {
                var result = _labelManager.LabelData(UserId);
                if (result.Count() >= 1)
                    return Ok(new { Status = true, Message = "Label Data!", Data = result });
                else
                    return BadRequest(new { Status = false, Message = "Label is Empty!" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
