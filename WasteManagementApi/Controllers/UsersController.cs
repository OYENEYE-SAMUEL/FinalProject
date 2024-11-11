using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Invalid Login Request" });
            }
            var response = await _userService.Login(request);
            if (!response.Status)
            {
                return Unauthorized(new { Message = response.Message });
            }
            /*var userResponse = response.Data;
            var user = new User
            {
                Email = userResponse.Email,
                Password = userResponse.Password,
                FullName = userResponse.FullName,
                Role = userResponse.Role
            };

            var token = _authService.GenerateToken(user);*/


            /*string redirectUrl =  user.Role.Name switch
            {
                "Customer" => Url.Action("Customer"),
                "Manager" => Url.Action("Manager"),
                "Admin" => Url.Action("Admin"),
                _ => Url.Action("Login", "User") 
            };*/



            return Ok(new TokenDto
            {
                Token = response.Data.Token,
                Message = "Login Successfully"
            });
        }
    }
}
