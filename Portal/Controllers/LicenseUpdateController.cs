using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;
using System.Net.Http;
using System.Text;

namespace Portal.Controllers
{
    public class LicenseUpdateController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _url = new Uri("http://localhost:5155");

        public LicenseUpdateController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
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
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, LicenseViewModels model)
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
                async Task<HttpResponseMessage> PutAsync(string endpoint, object body)
                {
                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return await _httpClient.PutAsync($"{_httpClient.BaseAddress}License/{id}", content);
                }

                // Update License details using external API
                var licenseResponse = await PutAsync("License", model);

                if (!licenseResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to create license.");
                    return View(model);
                }

                // Save Document details using external API
                // Helper method for making POST calls
                async Task<HttpResponseMessage> PostAsync(string endpoint, object body)
                {
                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync($"{_httpClient.BaseAddress}{endpoint}", content);
                }
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

                return RedirectToAction("Create", "Payment");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the license.");
                return View(model);
            }
        }
    }
}
