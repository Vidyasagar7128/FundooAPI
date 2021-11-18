using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }
        public string Register(RegisterModel userModel)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == userModel.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    if (userModel.FirstName != null && userModel.LastName != null && userModel.Email != null && userModel.Password != null)
                    {
                        this.userContext.Add(userModel);
                        this.userContext.SaveChanges();
                        return "Registration Done!";
                    }
                    return "Registration Failed!";
                }
                else
                    return "Email already Exist!";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegisterModel Login(LoginModel loginDetails)
        {
            var checkUser = this.userContext.Users.Where(e => e.Email == loginDetails.Email && e.Password == loginDetails.Password).FirstOrDefault();
            if (checkUser != null)
                return checkUser;
            else
                return null;
        }
    }
}
