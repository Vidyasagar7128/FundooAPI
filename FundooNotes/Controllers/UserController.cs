using FundoManager.Interfaces;
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
        [Route("api/login")]
        public IActionResult LogIn([FromBody] LoginModel loginDetails)
        {
            try
            {
                var result = this._userManager.LoginUser(loginDetails);
                if (result.Equals("Login Succesfull."))
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest(result);
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
       
    }
}
