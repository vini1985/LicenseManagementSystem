using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Portal.Controllers
{
    // LicenseController handles CRUD operations for licenses by communicating with an external API.
    // It is secured to allow access only to users with "Admin" or "User" roles.
    // All actions are asynchronous to improve performance.
    
    [Authorize(Roles = "Admin,User")]
    public class LicenseController : Controller
    {
        // GET: LicenseController
        private readonly HttpClient _httpClient;
        Uri _url = new Uri("http://localhost:5155");
        public LicenseController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}License");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to load license data.");
                    return View(new List<LicenseViewModels>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var licenses = JsonConvert.DeserializeObject<List<LicenseViewModels>>(json)
                               ?? new List<LicenseViewModels>();

                return View(licenses);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while loading licenses.");
                return View(new List<LicenseViewModels>());
            }
        }


        // GET: LicenseController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: LicenseController/Create
        public ActionResult Create()
        {
            ViewBag.LicenseId = Guid.NewGuid().ToString();
            ViewBag.TenantId = Guid.NewGuid().ToString();
            return View();
        }

        // POST: LicenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LicenseViewModels model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Save Document in the server's file system
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, model.DocumentInfo.FileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.DocumentInfo.CopyToAsync(stream);
                }

                // Helper method for making POST calls
                async Task<HttpResponseMessage> PostAsync(string endpoint, object body)
                {
                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync($"{_httpClient.BaseAddress}{endpoint}", content);
                }

                // Save License details using external API
                var licenseResponse = await PostAsync("License", model);

                if (!licenseResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to create license.");
                    return View(model);
                }

                // Save Document details using external API
                var documentVm = new DocumentViewModels
                {
                    DocumentId = Guid.NewGuid(),
                    LicenseId = model.LicenseId,
                    DocumentType = model.DocumentType,
                    DocumentName = model.DocumentName,
                    DocumentPath = $"wwwroot/uploads/{model.DocumentInfo.FileName}",
                    UploadedAt = DateTime.UtcNow
                };

                await PostAsync("Documents", documentVm);

                // After creating a license, it redirects to the Payment creation process.
                HttpContext.Session.SetString("LicenseId", model.LicenseId.ToString());

                return RedirectToAction(nameof(Create),"Payment");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the license.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}License/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to load license data.");
                    return View(new LicenseViewModels());  // return single object
                }

                var json = await response.Content.ReadAsStringAsync();
                var license = JsonConvert.DeserializeObject<LicenseViewModels>(json);

                return View(license);  // return ONE model
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while loading licenses.");
                return View(new LicenseViewModels());  // return empty model
            }
        }

        // POST: LicenseController/Edit/5
        //Based on the License ID update license inforamtion.
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id,LicenseViewModels model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                async Task<bool> PostToApiAsync(string endpoint, object payload)
                {
                    var json = JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}{endpoint}/{id}", httpContent);
                    return response.IsSuccessStatusCode;
                }

                var isLicenseCreated = await PostToApiAsync("License", model);
                if (!isLicenseCreated)
                {
                    ModelState.AddModelError("", "Unable to update license.");
                    return View(model);
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An unexpected error occurred: {ex.Message}");
                return View(model);
            }
        }

        // GET: LicenseController/Delete/5
        //Based on the License ID, it deletes the license by making a DELETE request to the external API.
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}License/{id}");
            if (response == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
