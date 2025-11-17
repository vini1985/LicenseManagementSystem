using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Portal.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PaymentController : Controller
    {
        // GET: PaymentController
        private readonly HttpClient _httpClient;
        Uri _url = new Uri("http://localhost:5155");
        public PaymentController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}Payments");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to load payment data.");
                    return View(new List<PaymentViewModels>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var payments = JsonConvert.DeserializeObject<List<PaymentViewModels>>(json)
                               ?? new List<PaymentViewModels>();

                return View(payments);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while loading payments.");
                return View(new List<PaymentViewModels>());
            }
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {
            string licenseId = HttpContext.Session.GetString("LicenseId");
            ViewBag.LicenseId = licenseId;
            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentViewModels model)
        {
            try
            {
                // Helper method for making POST calls
                async Task<HttpResponseMessage> PostAsync(string endpoint, object body)
                {
                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync($"{_httpClient.BaseAddress}{endpoint}", content);
                }

                // Save Payment details
                var paymentResponse = await PostAsync("Payments", model);

                if (!paymentResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Payment Failed.");
                    return View(model);
                }
                if(paymentResponse.IsSuccessStatusCode)
                {
                    //API call to trigger and save notification emails
                    var emailVm = new EmailViewModels
                    {
                        LicenseId = model.LicenseId,
                        Recipient = "test@test.com",
                        Subject = "Payment Confirmation",
                        Message = $"Your payment of {model.Amount} has been successfully processed.",
                        SenderAddress = "no-reply@yourdomain.com",
                        CreatedAt = DateTime.UtcNow
                    };

                    await PostAsync("Notifications", emailVm);
                }
                return RedirectToAction(nameof(Index),"License");
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
