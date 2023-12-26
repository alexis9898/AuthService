using DAL.Data;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _context;

        public UserRepository(AppDataContext context)
        {
            _context = context;
        }

        //get all
        public async Task<List<UserDetail>> GetUsers()
        {
            var workers = await _context.Users.ToListAsync();
            return workers;
        }

        //get all by filter
        public async Task<List<UserDetail>> GetFilterUsers(UserDetail User)
        {
            var workers = await _context.Users.Where(x=>
                    //User.Role != null? x.Role==User.Role:x.Role!=null &&
                    ((User.Name != null) ? (x.Name.ToLower().Contains(User.Name.ToLower())) : x.Name != null)
                )
                .ToListAsync();
            

            return workers;
        }

        //get one by Id
        public async Task<UserDetail> GetUser(int id)
        {
            var User = await _context.Users.Where(x=>x.Id==id).FirstOrDefaultAsync();
            return User;
        }

        //get one by UserId
        public async Task<UserDetail> GetUserByAccountId(string id)
        {
            var User = await _context.Users.Where(x => x.AccountId == id).FirstOrDefaultAsync();
            return User;
        }

        //add
        public async Task<UserDetail> AddUser(UserDetail User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return User;
        }

        //update
        public async Task UpdateUser(UserDetail User)
        {
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
            return;
        }

        //remove
        public async Task RemoveUser(UserDetail User)
        {
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return;
        }

    }
}
