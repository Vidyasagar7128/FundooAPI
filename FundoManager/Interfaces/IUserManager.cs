using FundooModels;

namespace FundoManager.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel userData);
        string LoginUser(LoginModel login);
        string SendEmailResetPassword(string email);
    }
}