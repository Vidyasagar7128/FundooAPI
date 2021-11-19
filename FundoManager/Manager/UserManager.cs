using System;
using System.Collections.Generic;
using System.Text;
using FundoManager.Interface;
using FundooModels;
using FundooRepository.Interfaces;

namespace FundoManager.Manager
{
    public class UserManager : IUserManager
    {
        private IUserRepository _repository;
        public UserManager(IUserRepository repository)
        {
            this._repository = repository;
        }
        public string Register(RegisterModel userData)
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
        public RegisterModel LoginUser(LoginModel loginDetails)
        {
            try
            {
                return this._repository.Login(loginDetails);
            }catch(Exception e)
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
        public string ChangePasswordUsingConfirmPassword(ForgotPassword forgotPassword)
        {
            try
            {
                return this._repository.ChangePasswordUsingPassword(forgotPassword);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
