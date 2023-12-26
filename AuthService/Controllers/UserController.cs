using BLL.Interface;
using BLL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Delivery_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetUserByFilter([FromBody] UserModel userModel)
        {
            try
            {
                var workers=await _userService.GetFilterUsers(userModel);
                if (workers == null)
                    return Ok("bad req");

                return Ok(workers);
            }
            catch (System.Exception)
            {
                //return BadRequest();
                throw;
            }
        }
    }
}
