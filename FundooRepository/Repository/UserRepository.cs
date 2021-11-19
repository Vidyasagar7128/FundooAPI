using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            Configuration = configuration;
            this._userContext = userContext;
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public string Register(RegisterModel userModel)
        {
            try
            {
                var validEmail = this._userContext.Users.Where(x => x.Email == userModel.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    if (userModel.FirstName != null && userModel.LastName != null && userModel.Email != null && userModel.Password != null)
                    {
                        var pass = EncryptPassword(userModel.Password);
                        userModel.Password = pass;
                        this._userContext.Add(userModel);
                        this._userContext.SaveChanges();
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
        /// <summary>
        /// Encrypt Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncryptPassword(string password)
        {
            Byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("fund0@Notes"));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data,0,data.Length);
                    return Convert.ToBase64String(result, 0, result.Length).ToString();
                }
            }
        }
        /// <summary>
        /// Login User using Credentials
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns></returns>
        public RegisterModel Login(LoginModel loginDetails)
        {
            try
            {
                var encPassword = EncryptPassword(loginDetails.Password);
                var checkUser = this._userContext.Users.Where(e => e.Email == loginDetails.Email && e.Password == encPassword).FirstOrDefault();
                if (checkUser != null)
                    return checkUser;
                else
                    return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string SendEmailforResetPassword(string email)
        {
            try
            {
                var checkEmail = this._userContext.Users.Where(e => e.Email == email).FirstOrDefault();
                if (checkEmail != null)
                    return ChangePassword();
                else
                    return "Email does not Exist!";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ChangePassword()
        {
            return "Hello ChangePassword method Calling";
        }
        /// <summary>
        ///Change Password Using Password & Confirm Password
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        public string ChangePasswordUsingPassword(ForgotPassword forgotPassword)
        {
            try
            {
                if (forgotPassword.Password == forgotPassword.ConfirmPassword)
                {
                    var matchPassword = this._userContext.Users.Where(e => e.Password == forgotPassword.OldPassword).FirstOrDefault();
                    if (matchPassword != null)
                    {
                        matchPassword.Password = forgotPassword.Password;
                        this._userContext.Update(matchPassword);
                        this._userContext.SaveChanges();
                        return "Password Changed Succesfully!";
                    }
                    else
                        return "Something went Wrong!";
                }
                else
                    return "Password Not Matching";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
