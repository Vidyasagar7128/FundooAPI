using FundooModels;

namespace FundoManager.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel userData);
        RegisterModel LoginUser(LoginModel login);
    }
}