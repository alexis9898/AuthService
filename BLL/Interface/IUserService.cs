using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUser(int id);
        Task<UserModel> GetUserByAccountId(string id);

        Task<List<UserModel>> GetFilterUsers(UserModel WorkerModel);
        Task<UserModel> AddUser(UserModel WorkerModel);
        Task<UserModel> UpdateUser(int id,UserModel WorkerModel);
        Task<bool> RemoveUser(int id);
    }
}
