// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundoManager.Manager
{
    using System;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using FundooRepository.Interfaces;

    /// <summary>
    /// UserManager class for handle UserRepository actions
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// Declare IUserRepository for perform operations on it
        /// </summary>
        private readonly IUserRepository _repository;

        /// <summary>
        /// for assign to private variable
        /// </summary>
        /// <param name="repository">passing IUserRepository variable</param>
        public UserManager(IUserRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userData">passing SignUpModel</param>
        /// <returns>register or not</returns>
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

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginDetails">Passing LoginModel</param>
        /// <returns>JWT Token</returns>
        public string LoginUser(LoginModel loginDetails)
        {
            try
            {
                return this._repository.Login(loginDetails);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// passing Email for jwt
        /// </summary>
        /// <param name="email">passing Email for JWT</param>
        /// <returns>return jWT token</returns>
        public string GetJwtToken(string email)
        {
            try
            {
                return this._repository.JwtToken(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Send Email ResetPassword
        /// </summary>
        /// <param name="email">passing email as string</param>
        /// <returns>sent or not</returns>
        public string SendEmailResetPassword(string email)
        {
            try
            {
                return this._repository.SendEmailforResetPassword(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Reset Password using details
        /// </summary>
        /// <param name="resetPasswordModel">passing ResetPasswordModel</param>
        /// <returns>changed or not</returns>
        public async Task<string> ResetPass(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return await this._repository.ResetPassword(resetPasswordModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
