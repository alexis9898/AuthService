using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserRepository
    {
        Task<List<UserDetail>> GetUsers();
        Task<List<UserDetail>> GetFilterUsers(UserDetail User);
        Task<UserDetail> GetUserByAccountId(string id);

        Task<UserDetail> GetUser(int id);
        Task RemoveUser(UserDetail User);
        Task UpdateUser(UserDetail User);
        Task<UserDetail> AddUser(UserDetail User);
    }
}
