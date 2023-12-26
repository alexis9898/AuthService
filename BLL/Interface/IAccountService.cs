using BLL.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IAccountService
    {
        Task<bool> SignUp(SignUpModel signUpModel);
        Task<AccountModel> LoginAsync(SignInModel signInModel);
        Task<UserModel> FindUserByAccuontId(string userId);

    }
}
