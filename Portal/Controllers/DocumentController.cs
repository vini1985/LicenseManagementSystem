using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;

namespace Portal.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class DocumentController : Controller
    {
        // GET: DocumentController
        private readonly HttpClient _httpClient;
        Uri _url = new Uri("http://localhost:5155");
        public DocumentController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}Documents");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to load document data.");
                    return View(new List<DocumentViewModels>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var documents = JsonConvert.DeserializeObject<List<DocumentViewModels>>(json)
                               ?? new List<DocumentViewModels>();

                return View(documents);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while loading documents.");
                return View(new List<DocumentViewModels>());
            }
        }

        // GET: DocumentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DocumentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DocumentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DocumentController/Edit/5
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

        // GET: DocumentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DocumentController/Delete/5
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
