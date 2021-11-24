using FundooModels;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Interfaces
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }

        string Register(SignUpModel signupModel);
        object Login(LoginModel loginModel);
        string SendEmailforResetPassword(string email);
    }
}