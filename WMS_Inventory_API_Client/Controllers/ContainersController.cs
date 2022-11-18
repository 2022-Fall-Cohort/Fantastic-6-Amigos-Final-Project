using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WMS_Inventory_API_Client.Models;
using WMS_Inventory_API_Client.Services.Interfaces;

namespace WebMVC_API_Client.Controllers
{
    public class ContainerController : Controller
    {
        private IContainerService? _service;

        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7153/api/Containers/";

        public ContainerController(IContainerService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Jim's API");
        }

        // Example: https://localhost:7153/api/Containers
        public async Task<IActionResult> Index()
        {
            var response = await _service.FindAll();

            return View(response);
        }

        // GET: Container/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var container = await _service.FindOne(id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // GET: Container/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Container/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Description,StorageLocationID")] Container container)
        {
            container.Id = null;
            var resultPost = await client.PostAsync<Container>(requestUri, container, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: Container/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var container = await _service.FindOne(id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Container/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Description,StorageLocationID")] Container container)
        {
            if (id != container.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<Container>(requestUri + container.Id.ToString(), container, new JsonMediaTypeFormatter());
            return RedirectToAction(nameof(Index));
        }

        // GET: Container/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var container = await _service.FindOne(id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Container/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultDelete = await client.DeleteAsync(requestUri + id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}