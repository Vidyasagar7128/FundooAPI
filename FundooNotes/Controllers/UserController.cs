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
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public UserController(IUserManager manager)
        {
            this._userManager = manager;
        }
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
                            Message = result,
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
                        UserId = result.Id.ToString()
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
    }
}
