using Application.DTOs;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;
using WasteSystemApp.Models;

namespace WasteSystemApp.Controllers
{
    public class CommunityController : Controller
    {
        private readonly HttpClient _client;
    

        public CommunityController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.Timeout = TimeSpan.FromMinutes(5); 
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterCommunity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCommunity(CommunityRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Community/registercommunity", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Login", "Login");
                }

                else
                {
                    TempData["RegError"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("RegisterCommunity");
                }

            }
            catch (Exception ex)
            {
                TempData["ExceptionError"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("RegisterCommunity");
            }
          
        }

        [HttpGet]
        public IActionResult CreatePlan()
        {
            var response = _client.GetAsync("https://localhost:7068/api/Subscriptions/pendingplan");
            if (response.Result.IsSuccessStatusCode)
            {
                var getDetails = response.Result.Content.ReadAsStringAsync();
                var allSubcribe = JsonSerializer.Deserialize<ICollection<SubscriptionViewModel>>(getDetails.Result, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

                allSubcribe.Select(e => new
                {
                    Id = e.Id,
                    Price = e.Price,
                    AmountPaid = e.AmountPaid,
                    Frequency = e.Frequency,
                    PlanType = e.CurrentPlanType,
                    CurrentPlanDateStart = e.CurrentPlanDateStart
                });
                var selectSub = allSubcribe.Select(x => new
                {
                    Id = x.Id,
                    DisplayText = $"{x.Price} {x.Frequency} {x.CurrentPlanType}"
                });
                ViewBag.allsubscribe = new SelectList(selectSub, "Id", "DisplayText");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(SubscriptionRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("Subscriptions/MakeSubcription", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["plansuccess"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["planerror"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

            }
            catch (Exception ex)
            {
                TempData["planerror"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("CreatePlan");
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
            TempData["PlanError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }


        [HttpGet]
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
            TempData["AllError"] = response.Content.ReadAsStringAsync();
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
                    TempData["WasteError"] = await response.Content.ReadAsStringAsync();
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
            var response = await _client.GetAsync("https://localhost:7068/api/WasteCollections/GetAllWasteRequest");
            if (response.IsSuccessStatusCode)
            {
                var request = await response.Content.ReadAsStringAsync();
                var getWaste = JsonSerializer.Deserialize<ICollection<WasteCollectionResponseModel>>(request, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(getWaste);
            }
            TempData["RequestError"] = response.Content.ReadAsStringAsync();
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
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/WasteReports/create", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["ReportError"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("MakeReport");
                }

            }
            catch (Exception ex)
            {
                TempData["ExceptionError"] = $"An error occurred: {ex.Message}";
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
            TempData["AllError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public IActionResult MakePayment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment(PaymentRequestModel model)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Payments/makepayment", model);
                if (response.IsSuccessStatusCode)
                {
                    TempData["PaySuccess"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    TempData["PayError"] = await response.Content.ReadAsStringAsync();
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Payment");
            }
        }
        
       
    }
}
