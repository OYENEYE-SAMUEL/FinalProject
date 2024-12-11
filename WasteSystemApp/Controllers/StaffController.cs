using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WasteSystemApp.Controllers
{
    public class StaffController : Controller
    {
        private readonly HttpClient _client;

        public StaffController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.Timeout = TimeSpan.FromMinutes(5); 
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        public async Task<IActionResult> LocationAssignTo(Guid staffId)
        {
            var response = await _client.GetAsync($"https://localhost:7068/api/WasteCollections/getwasteassign");
            if (response.IsSuccessStatusCode)
            {
                var collections = await response.Content.ReadAsStringAsync();
                var report = JsonSerializer.Deserialize<ICollection<WasteCollectionResponseModel>>(collections, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(report);
            }
            TempData["AssignError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }
    }
}
