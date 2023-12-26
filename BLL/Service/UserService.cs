using AutoMapper;
using BLL.Interface;
using BLL.Model;
using DAL.Data;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //get all
        public async Task<List<UserModel>> GetUsers()
        {
            var usersModel = await _userRepository.GetUsers();
            return _mapper.Map<List<UserModel>>(usersModel);
        }
        //get one by id
        public async Task<UserModel> GetUser(int id)
        {
            var userModel = await _userRepository.GetUser(id);
            return _mapper.Map<UserModel>(userModel);
        }

        //get one by userId
        public async Task<UserModel> GetUserByAccountId(string id)
        {
            var UserModel = await _userRepository.GetUserByAccountId(id);
            return _mapper.Map<UserModel>(UserModel);
        }

        //get all by filter
        public async Task<List<UserModel>> GetFilterUsers(UserModel UserModel)
        {
            bool valid = IfValid(UserModel);
            if (valid==false)
                return null;
            var user= _mapper.Map<UserDetail>(UserModel);
            List<UserDetail> workers = await _userRepository.GetFilterUsers(user);
            List<UserModel> sendUserModels= _mapper.Map<List<UserModel>>(workers);
            return sendUserModels;
        }

        //add
        public async Task<UserModel> AddUser(UserModel userModel)
        {
            var user = await _userRepository.AddUser(_mapper.Map<UserDetail>(userModel));
            userModel.Id = user.Id;
            return userModel;
        }

        //update put
        public async Task<UserModel> UpdateUser(int id,UserModel userModel)
        {
            UserDetail user = await _userRepository.GetUser(id);
            if(user == null)
                return null;

            user.Id = id;
            user.Phone=userModel.Phone;
            user.Name=userModel.Name;
            user.AccountId=userModel.AccountId;

            //if(user.Role != Role.Manager || user.Role != Role.DeliveryPersons)
            //    return null;
            //user.Role=userModel.Role;

            await _userRepository.UpdateUser(user);
            return _mapper.Map<UserModel>(user);
        }


        //remove
        public async Task<bool> RemoveUser(int id)
        {
            UserDetail user = await _userRepository.GetUser(id);
            if (user == null)
                return false;

            await _userRepository.RemoveUser(user);
            return true;
        }
        public bool IfValid(UserModel WorkerModel)
        {
            if(WorkerModel == null)
                return false ;
            //if(WorkerModel.Role != Role.Manager && WorkerModel.Role !=Role.DeliveryPersons)
            //    return false ;

            return true ;
        }

    }
}
