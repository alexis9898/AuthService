using BLL.Interface;
using BLL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Delivery_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IAccountService _accountReposity;
        public AccountController(IAccountService accountReposity)
        {
            _accountReposity=accountReposity;
        }

        [HttpPost("signUp")]
        /*[Authorize(Roles = Role.Manager)]*/
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                var result = await _accountReposity.SignUp(signUpModel);
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            try
            {
                var result = await _accountReposity.LoginAsync(signInModel);
                if (result == null)
                {
                    return Unauthorized(new {message = "FAILED_LOGIN" });
                }
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
