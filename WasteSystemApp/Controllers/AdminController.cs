using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WasteSystemApp.Models;

namespace WasteSystemApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _client;

        public AdminController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.Timeout = TimeSpan.FromMinutes(5); 
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStaff(StaffRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Staffs/registerstaff", request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["RegSuccess"] = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("DashBoard");
                }

                else
                {
                    ViewBag.errorException = await response.Content.ReadAsStringAsync();
                    return View(request);
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = $"An error occurred: {ex.Message}";
                return View(request);
            }
        }

        public async Task<IActionResult> AllStaffs()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Staffs/allstaffs");
            if (response.IsSuccessStatusCode)
            {
                var staff = await response.Content.ReadAsStringAsync();
                var getStaff = JsonSerializer.Deserialize<ICollection<StaffResponseModel>>(staff, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(getStaff);
            }
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public async Task<IActionResult> AllIndividuals()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Individuals/allpersons");
            if (!response.IsSuccessStatusCode)
            {
                TempData["message"] = response.Content.ReadAsStringAsync();
                return RedirectToAction("DashBoard");
            }
            var person = await response.Content.ReadAsStringAsync();
            var getPerson = JsonSerializer.Deserialize<ICollection<IndividualResponseModel>>(person, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
            return View(getPerson);
            
        }

        [HttpGet]
        public async Task<IActionResult> AllAgent()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/GovernmentAgent/allgovernments");
            if (response.IsSuccessStatusCode)
            {
                var agent = await response.Content.ReadAsStringAsync();
                var getAgent = JsonSerializer.Deserialize<ICollection<GovernmentAgentResponseModel>>(agent, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

                return View(getAgent);
            }
            TempData["AgentError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }


        [HttpGet]
        public IActionResult CreatePlan()
        {
               /* var response = _client.GetAsync("https://localhost:7068/api/Subscriptions/pendingplan");
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
                }*/
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(SubscriptionRequestModel request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("https://localhost:7068/api/Subscriptions/createplan", request);
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
        public async Task<IActionResult> PendingPlan()
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
        public async Task<IActionResult> ExpiredPlans()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Subscriptions/allexpiredplan");
            if (response.IsSuccessStatusCode)
            {
                var plan = await response.Content.ReadAsStringAsync();
                var getPlan = JsonSerializer.Deserialize<ICollection<SubscriptionResponseModel>>(plan, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                return View(getPlan);
            }
            TempData["ExpiredError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }



        [HttpGet]
        public async Task<IActionResult> PendingContracts()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Contracts/pendingcontracts");
            if (response.IsSuccessStatusCode)
            {
                var contract = await response.Content.ReadAsStringAsync();
                var getContract = JsonSerializer.Deserialize<ICollection<ContractResponseModel>>(contract, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(getContract);
            }
            TempData["PendingError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public async Task<IActionResult> ActiveContracts()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Contracts/activecontract");
            if (response.IsSuccessStatusCode)
            {
                var contract = await response.Content.ReadAsStringAsync();
                var getContract = JsonSerializer.Deserialize<ICollection<ContractResponseModel>>(contract, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(getContract);
            }
            TempData["ActieError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }

      
        public async Task<IActionResult> AllCommunity()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/Communities/allcommunities");
            if (response.IsSuccessStatusCode)
            {
                var community = await response.Content.ReadAsStringAsync();
                var getCommunity = JsonSerializer.Deserialize<ICollection<CommunityResponseModel>>(community, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true });
                return View(getCommunity);
            }
            TempData["CommunityError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }

        public async Task<IActionResult> AssignRequest()
        {
            var response = await _client.GetAsync("https://localhost:7068/api/WasteReport/assignrequest");
            if (response.IsSuccessStatusCode)
            {
                var wasteReport = await response.Content.ReadAsStringAsync();
                var getWasteReport = JsonSerializer.Deserialize<ICollection<WasteCollectionResponseModel>>(wasteReport, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                return View(getWasteReport);
            }
            TempData["AssignError"] = response.Content.ReadAsStringAsync();
            return RedirectToAction("DashBoard");
        }
    }
}
