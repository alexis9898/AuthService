using BLL.Interface;
using BLL.Model;
using DAL.Data;
using DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDataContext _context;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            AppDataContext context,
            IUserService workerService ,
            IConfiguration configuration)
        {
            _userManager = userManager;   //userManager => context 
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _userService = workerService;
            _configuration = configuration;
        }

        public async Task<bool> SignUp(SignUpModel signUpModel)
        {
            var createdUser =await SaveUserAccunt(signUpModel);
            if (createdUser == null) return false;
            var createWorker = await AddUser(signUpModel, createdUser.Id);
            if (createWorker == null) return false;
            return true;
        }

        public async Task<AccountModel> LoginAsync(SignInModel signInModel)
        {
            var result= await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,false,false);
            

            if (!result.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(signInModel.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            var id = user.Id;
            //var token = generateToken(signInModel.UserName,roles[0]);
            var token = generateToken(signInModel.UserName,"");
            var _token = new JwtSecurityTokenHandler().WriteToken(token);
            var exp = token.ValidTo.Date;

            //return new UserModel() { Id = id, Name = signInModel.UserName, Role = roles[0], _token = _token, _tokenExpirationDate = exp};
            return new AccountModel() { Id = id, Name = signInModel.UserName, _token = _token, _tokenExpirationDate = exp};
        }

        private JwtSecurityToken generateToken(string UserName, string role)
        {
            var tokenDetails = new string[2];
            var authClaims = new List<Claim>
            {
                //new Claim("id", "12345"),
                new Claim(ClaimTypes.Name, UserName),
                //new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiences"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature));

            /*tokenDetails[0] = new JwtSecurityTokenHandler().WriteToken(token);
            var a = token.ValidTo;*/
            return token;
        }


        public async Task<AppUser> SaveUserAccunt(SignUpModel signUp)
        {
            var newUser = new AppUser()
            {
                UserName = signUp.UserName,
                PhoneNumber = signUp.Phone,
            };

            //if (signUp.Role != Role.Manager && signUp.Role != Role.DeliveryPersons)
            //   return null;
            var result = await _userManager.CreateAsync(newUser, signUp.Password);

            if (result.Succeeded)
                {
                    var createduser = await _userManager.FindByNameAsync(signUp.UserName);
                    if (createduser != null)
                    {
                        //await _userManager.addtoroleasync(createduser, signup.role);
                        return createduser;
                    }
                    return null;
                }
                else return null;
        }

        public async Task<UserModel> AddUser(SignUpModel signUpModel,string AccountId) 
        {
            UserModel userModel = new UserModel()
            {
                AccountId = AccountId,
                Name = signUpModel.UserName,
                //Role = signUpModel.Role,
                Phone = signUpModel.Phone,
            };
            var user=await _userService.AddUser(userModel);
            if(user==null)
                return null;
            return user;
        }

        public async Task<UserModel> FindUserByAccuontId(string userId)
        {
            UserModel userModel = await _userService.GetUserByAccountId(userId);
            if (userModel == null)
                return null;
          
            return userModel;
        }

        //public async Task<UserDataModel> FindUserById(string id)
        //{
        //    var user=await _userManager.FindByIdAsync(id);
        //    if(user == null)
        //        return null;
        //    var role = await _userManager.GetRolesAsync(user);
        //    var userModel = new UserDataModel()
        //    {
        //        Id = user.Id,
        //        Name = user.UserName,
        //        Phone = user.PhoneNumber,
        //        Role = role[0]
        //    };
        //    return userModel;
        //}
        
        
    }
}
