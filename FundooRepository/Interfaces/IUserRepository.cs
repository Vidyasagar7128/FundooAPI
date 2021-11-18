using FundooModels;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Interfaces
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }

        string Register(RegisterModel userModel);
        RegisterModel Login(LoginModel loginModel);
    }
}