using Models.Users;
using ViewModels.Userr;

namespace Reposotries
{
    public interface IUserRepository
    {
        Task<AuthModel> SignUp(SignUpModel signUpModel);

        Task<AuthModel> Login(LoginModel loginModel);


    }
}
