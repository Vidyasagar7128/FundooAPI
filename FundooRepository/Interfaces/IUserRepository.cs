using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface IUserRepository
    {
        string Register(SignUpModel signupModel);
        string Login(LoginModel loginModel);
        string SendEmailforResetPassword(string email);
        string JwtToken(string Email);
        Task<string> ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}