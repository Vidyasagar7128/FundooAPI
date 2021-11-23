using Experimental.System.Messaging;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        public string Register(SignUpModel signupModel)
        {
            try
            {
                var validEmail = this._userContext.Users.Where(x => x.Email == signupModel.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    if (signupModel.FirstName != null && signupModel.LastName != null && signupModel.Email != null && signupModel.Password != null)
                    {
                        var pass = EncryptPassword(signupModel.Password);
                        signupModel.Password = pass;
                        this._userContext.Users.Add(signupModel);
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
        public string Login(LoginModel loginDetails)
        {
            try
            {
                if (this._userContext.Users.Where(e => e.Email == loginDetails.Email).FirstOrDefault() != null)
                {
                    if (this._userContext.Users.Where(e => e.Password == EncryptPassword(loginDetails.Password)).FirstOrDefault() != null)
                        return "Login Succesful";
                    else
                        return "Password is not Matching";
                }
                else if (this._userContext.Users.Where(e => e.Email == loginDetails.Email || e.Password == EncryptPassword(loginDetails.Password)).FirstOrDefault() == null)
                    return "Email & Password are not Matching";
                else
                    return "Email is not Matching";
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
                    return ChangePassword(email);
                else
                    return "Email does not Exist!";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string ChangePassword(string email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(this.Configuration.GetValue<string>("EmailId"));
                mail.To.Add(email);
                mail.Subject = "[FunDoo] Password Reset Link";
                mail.Body = "Do you want to change your Password!";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
                smtpClient.Credentials = new System.Net.NetworkCredential(this.Configuration.GetValue<string>("EmailId"),this.Configuration.GetValue<string>("Password"));
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                return "Email Sent Succesfully!";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
