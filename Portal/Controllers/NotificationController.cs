using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Portal.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class NotificationController : Controller
    {
        // GET: NotificationController
        private readonly HttpClient _httpClient;
        Uri _url= new Uri("http://localhost:5155/Notifications");
        public NotificationController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            List<NotificationViewModels> notificationData = new List<NotificationViewModels>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;
            if (response.IsSuccessStatusCode) { 
                string data = response.Content.ReadAsStringAsync().Result;
                notificationData = JsonConvert.DeserializeObject<List<NotificationViewModels>>(data);
            }
            return View(notificationData);
        }

        // GET: NotificationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotificationViewModels model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(data, Encoding.UTF8,"application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                return View();
            }
            return View();
        }

        // GET: NotificationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationController/Edit/5
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

        // GET: NotificationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationController/Delete/5
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
