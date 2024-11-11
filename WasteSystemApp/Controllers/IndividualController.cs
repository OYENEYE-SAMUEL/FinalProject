using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WasteSystemApp.Controllers
{
    public class IndividualController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7068/api");
        private readonly HttpClient _client;

        public IndividualController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = baseAddress;
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPerson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPerson(IndividualRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Individuals/register", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["error"] = await response.Content.ReadAsStringAsync();
                    return View("RegisterPerson");
                }
            }
            catch (Exception ex)
            {

                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("RegisterPerson");
            }
        }

       

        [HttpGet]
        public async Task<IActionResult> ViewPlan(Guid id)
        {
            var response = await _client.GetAsync($"https://localhost:7068/api/Subcriptons/GetPlan?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var plan = await response.Content.ReadAsStringAsync();
                var subcribe = JsonSerializer.Deserialize<SubscriptionResponseModel>(plan, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                return View(subcribe);
            }

            return RedirectToAction("DashBoard");
        }

        public async Task<IActionResult> ViewAllPlans()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Subscriptions/pendingplan");
            if (response.IsSuccessStatusCode)
            {
                var plan = await response.Content.ReadAsStringAsync();
                var getPlan = JsonSerializer.Deserialize<ICollection<SubscriptionResponseModel>>(plan, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                return View(getPlan);
            }
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public IActionResult WasteReqest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WasteRequest(WasteCollectionRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/WasteCollections/WasteRequest", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["wastesuccess"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["wasteerror"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("WasteRequest");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("WasteRequest");
            }
        }

        public async Task<IActionResult> AllRequest()
        {
            var response = await _client.GetAsync($"https://localhost:7068/api/WasteCollections/GetAllWasteRequest");
            if (response.IsSuccessStatusCode)
            {
                var request = await response.Content.ReadAsStringAsync();
                var getWaste = JsonSerializer.Deserialize<ICollection<WasteCollectionResponseModel>>(request, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(getWaste);
            }

            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public IActionResult MakeReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeReport(WasteReportRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/apiWasteReports/CreateReport", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["error"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("MakeReport");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("MakeReport");
            }
        }

        public async Task<IActionResult> GetAllReport()
        {
            var response = await _client.GetAsync($"https://localhost:7068/api/WasteReports/GetAllReport");
            if (response.IsSuccessStatusCode)
            {
                var getReport = await response.Content.ReadAsStringAsync();
                var report = JsonSerializer.Deserialize<ICollection<WasteReportResponseModel>>(getReport, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(report);
            }

            return RedirectToAction("DashBoard");
        }
    }
}
