// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using Microsoft.AspNetCore.Mvc;
    using StackExchange.Redis;

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
        /// <param name="userData">passing SignUpModel</param>
        /// <returns>IActionResult for register</returns>
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
            }
            catch (Exception e)
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
        /// <param name="loginDetails">passing LoginModel</param>
        /// <returns>IActionResult for LogIn</returns>
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
                    return this.Ok(new { Status = true, Data = data, Token = this._userManager.GetJwtToken(loginDetails.Email) });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>()
                {
                    Status = false,
                    Message = e.Message
                });
            }
        }

        /// <summary>
        /// Forget Password using Email
        /// </summary>
        /// <param name="email">email string</param>
        /// <returns>IActionResult for ForgetPasswordSendEmail</returns>
        [HttpPost]
        [Route("forgetpassword")]
        public IActionResult ForgetPasswordSendEmail([FromBody] string email)
        {
            try
            {
                var result = this._userManager.SendEmailResetPassword(email);
                if (result.Equals("Email does not Exist!"))
                {
                    return this.BadRequest(new ResponseModel<string>() 
                    { 
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
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>()
                {
                    Status = false,
                    Message = e.Message
                });
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="resetPasswordModel">passing ResetPasswordModel</param>
        /// <returns>async IActionResult for PasswordReset</returns>
        [HttpPut]
        [Route("resetpassword")]
        public async Task<IActionResult> PasswordReset([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var result = await this._userManager.ResetPass(resetPasswordModel);
                if (result.Equals("Password Changed!"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
