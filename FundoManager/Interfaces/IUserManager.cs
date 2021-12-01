using FundooModels;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface IUserManager
    {
        string Register(SignUpModel userData);
        string LoginUser(LoginModel login);
        string SendEmailResetPassword(string email);
        string GetJwtToken(string Email);
        Task<string> ResetPass(ResetPasswordModel resetPasswordModel);
    }
}