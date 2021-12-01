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
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    /// <summary>
    /// UserController for Users Actions
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// declaring ILogger variable for performing actions on users repo
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// declaring ILogger variable for Logging
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Constructor for UserController
        /// </summary>
        /// <param name="manager">passing IUserManager to assign its values to variables</param>
        /// <param name="logger">passing ILogger for displays activities of users</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this._userManager = manager;
            this._logger = logger;
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
                        this._logger.LogInformation($"Registration done with {userData.Email} Id.");
                        return this.Ok(new ResponseModel<string>()
                        {
                            Status = true,
                            Message = result
                        });
                    }
                    else
                    {
                        this._logger.LogWarning($"{result}");
                        return this.BadRequest(new ResponseModel<string>()
                        {
                            Status = false,
                            Message = result,
                        });
                    }
                }
                else
                {
                    this._logger.LogWarning($"Validation Error!");
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = "Validation Error!"
                    });
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
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
                    this._logger.LogInformation($"Welcome again {loginDetails.Email}");
                    return this.Ok(new { Status = true, Data = data, Token = this._userManager.GetJwtToken(loginDetails.Email) });
                }
                else
                {
                    this._logger.LogWarning("Something went Wrong!");
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error : {e.Message}");
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
                    this._logger.LogWarning(result.ToString());
                    return this.BadRequest(new ResponseModel<string>() 
                    { 
                        Status = false,
                        Message = "Email does not Exist!",
                        Data = "null"
                    });
                }
                else
                {
                    this._logger.LogInformation($"Email sent to {email}");
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
                this._logger.LogError(e.Message.ToString());
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
                    this._logger.LogInformation($"Password changed succesfully!");
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    this._logger.LogWarning("Something went Wrong!");
                    return this.BadRequest(new { Status = false, Message = "Something went Wrong!" });
                }
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error : {e.Message}");
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
