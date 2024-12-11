using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace WasteSystemApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        public LoginController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.Timeout = TimeSpan.FromMinutes(5); 

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserRequestModel request)
        {
           
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Users/Login", request);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["error"] = await response.Content.ReadAsStringAsync();
                    return View(request);
                }

                var data = await response.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<TokenDto>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            if (userData.Token == null)
            {
                TempData["AuthMessage"] = userData.Message;  
                return View(request);
            }

			HttpContext.Session.SetString("JWToken", userData.Token.ToString());
			var token = HttpContext.Session.GetString("JWToken");
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
			var role = jwtToken?.Claims.First(claim => claim.Type == "roles").Value;


			return role switch
			{
				"Admin" => RedirectToAction("DashBoard", "Admin"),
				"Community" => RedirectToAction("DashBoard", "Community"),
				"GovernmentAgent" => RedirectToAction("DashBoard", "GovernmentAgent"),
				"WasteTaker" => RedirectToAction("DashBoard", "Staff"),
				"Individual" => RedirectToAction("DashBoard", "Individual"),
				_ => View(request)
			};




			/* catch (Exception ex)
			 {
				 TempData["error"] = $"An error occurred: {ex.Message}";
				 return View(request);
			 }*/
		}

        /*var response = await _client.PostAsJsonAsync("Users/login", request);
        if (response.IsSuccessStatusCode)
        {
           var data = await response.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<UserResponseModel>(data);


            if (userData.Role.Name == "Admin")
            {
                return RedirectToAction("Admin", "DashBoard");
            }

            if (userData.Role.Name == "Community")
            {
                return RedirectToAction("Community", "DashBoard");
            }

            if (userData.Role.Name == "GovernmentAgent")
            {
                return RedirectToAction("GovernmentAgent", "DashBoard");
            }

            if (userData.Role.Name == "Staff")
            {
                return RedirectToAction("Staff", "DashBoard");
            }

            if (userData.Role.Name == "Individual")
            {
                return RedirectToAction("Individual", "DashBoard");
            }

            return View(request);
        }

        else
        {
                TempData["error"] = await response.Content.ReadAsStringAsync();
                return View(request);
        }*/
    }
    
}

