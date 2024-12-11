using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WasteSystemApp.Controllers
{
    public class GovernmentAgentController : Controller
    {
        private readonly HttpClient _client;

        public GovernmentAgentController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.Timeout = TimeSpan.FromMinutes(5); 
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterGovernment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterGovernment(GovernmentAgentRequestModel request)
        {
           /* try
            {*/
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/GovernmentAgent/register", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["RegError"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("RegisterGovernment");
                }

            
              /* TempData["error"] = $"An error occurred: {ex.Message}";*/
        }

        [HttpGet]
        public IActionResult MakeContract()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeContract(ContractRequestModel request)
        {
           
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Contract/createcontract", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["message"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("MakeContract");
                }
            
           /* catch (Exception ex)
            {

                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("MakeContract");
            }*/
        }

        [HttpGet]
        public async Task<IActionResult> ViewContract(Guid id)
        {
            var response = await _client.GetAsync($"https://localhost:7068/api/Contracts/GetContract?id ={id}");
            if (response.IsSuccessStatusCode)
            {
                var contract = await response.Content.ReadAsStringAsync();

                var contractDeserialize = JsonSerializer.Deserialize<ContractResponseModel>(contract);
                return View(contractDeserialize);
            }

            TempData["ContractError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");

        }
    }
}
