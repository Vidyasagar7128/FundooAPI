using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FundoManager.Interfaces;
using FundooModels;
using FundooRepository.Interfaces;

namespace FundoManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;
        public UserManager(IUserRepository repository)
        {
            this._repository = repository;
        }
        public string Register(SignUpModel userData)
        {
            try
            {
                return this._repository.Register(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string LoginUser(LoginModel loginDetails)
        {
            try
            {
                return this._repository.Login(loginDetails);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string GetJwtToken(string Email)
        {
            try
            {
                return this._repository.JwtToken(Email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string SendEmailResetPassword(string email)
        {
            try
            {
                return this._repository.SendEmailforResetPassword(email);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
