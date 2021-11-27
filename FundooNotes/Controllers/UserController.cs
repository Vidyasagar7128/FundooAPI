using FundoManager.Interfaces;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager manager)
        {
            this._userManager = manager;
        }
        /// <summary>
        /// for SignUp Controller Method
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup")]
        public IActionResult Register([FromBody] SignUpModel userData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = this._userManager.Register(userData);
                    if (result.Equals("Registration Done!"))
                    {
                        return this.Ok(new ResponseModel<string>()
                        {
                            Status = true,
                            Message = result
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
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = "Validation Error!"
                    });
                }
            }catch(Exception e)
            {
                return this.NotFound(new ResponseModel<string>()
                {
                    Status = false,
                    Message = e.Message
                });
            }
        }
        /// <summary>
        /// for Login Controller Method
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult LogIn([FromBody] LoginModel loginDetails)
        {
            try
            {
                var result = this._userManager.LoginUser(loginDetails);
                if (result.Equals("Login Succesfull."))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();

                    string firstName = database.StringGet("Firstname");
                    string lastName = database.StringGet("Lastname");
                    SignUpModel data = new SignUpModel { FirstName = firstName, LastName = lastName, Email = loginDetails.Email };
                    return this.Ok(new { Status = true, Data = data, Token = _userManager.GetJwtToken(loginDetails.Email) });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch(Exception e)
            {
                return this.NotFound(new ResponseModel<string>()
                {
                    Status = false,
                    Message = e.Message
                });
            }
        }
        [HttpPost]
        [Route("forgetpassword")]
        public IActionResult ForgetPasswordSendEmail([FromBody] string email)
        {
            try
            {
                var result = this._userManager.SendEmailResetPassword(email);
                if (result.Equals("Email does not Exist!"))
                {
                    return this.BadRequest(new ResponseModel<string>() { 
                        Status = false,
                        Message = "Email does not Exist!",
                        Data = "null"
                    });
                }
                else
                {
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = "Email sent Succesully!",
                        Data = result.ToString()
                    });
                }
            }catch(Exception e)
            {
                return this.NotFound(new ResponseModel<string>()
                {
                    Status = false,
                    Message = e.Message
                });
            }
        }
        [HttpPut]
        [Route("resetpassword")]
        public async Task<IActionResult> PasswordReset([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var result = await _userManager.ResetPass(resetPasswordModel);
                if (result.Equals("Password Changed!"))
                    return Ok(new { Status = true,Message = result });
                else
                    return BadRequest(new { Status = false, Message = "Something went Wrong!" });
            }
            catch(Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
