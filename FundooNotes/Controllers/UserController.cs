using FundoManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
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
        [Route("api/signup")]
        public IActionResult Register([FromBody] RegisterModel userData)
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
        [Route("api/login")]
        public IActionResult LogIn([FromBody] LoginModel loginDetails)
        {
            try
            {
                var result = this._userManager.LoginUser(loginDetails);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = result.FirstName.ToString(),
                        Data = result.Id.ToString()
                    });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = "User Not Found by this Details!"
                    });
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
        [Route("api/forgetpassword")]
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
        /// <summary>
        /// for Change Password Using Password & Confirm Password Controller Method
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/changepassword")]
        public IActionResult ChangePasswordUsingPassword([FromBody] ForgotPassword forgotPassword)
        {
            try
            {
                var result = this._userManager.ChangePasswordUsingConfirmPassword(forgotPassword);
                if(result.Equals("Password Changed Succesfully!"))
                {
                    return this.Ok(new ResponseModel<string>()
                    {
                        Status = true,
                        Message = "Password Changed Succesfully!",
                    });
                }else if(result.Equals("Something went Wrong!"))
                {
                    return this.NotFound(new ResponseModel<string>() {
                        Status = false,
                        Message = "Something went Wrong!",
                    });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = "Password Not Matching"
                    });
                }

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
