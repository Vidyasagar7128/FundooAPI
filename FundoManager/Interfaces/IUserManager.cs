using FundooModels;

namespace FundoManager.Interfaces
{
    public interface IUserManager
    {
        string Register(SignUpModel userData);
        string LoginUser(LoginModel login);
        string SendEmailResetPassword(string email);
    }
}