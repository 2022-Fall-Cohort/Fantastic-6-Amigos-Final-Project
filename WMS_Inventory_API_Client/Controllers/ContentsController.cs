using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WMS_Inventory_API_Client.Models;
using WMS_Inventory_API_Client.Services.Interfaces;

namespace WMS_Inventory_API_Client.Controllers
{
    public class ContentsController : Controller
    {
        private IContentService? _service;
        private IContainerService? _serviceContainer;


        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7153/api/Contents/";

        public ContentsController(IContentService service, IContainerService serviceContainer)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _serviceContainer = serviceContainer ?? throw new ArgumentNullException(nameof(serviceContainer));

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Jim's API");
        }

        // Example: https://localhost:7153/api/Contents
        public async Task<IActionResult> Index()
        {
            var response = await _service.FindAll();

            return View(response);
        }

        // GET: Content/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var content = await _service.FindOne(id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Content/Create
        public async Task<IActionResult> Create()
        {
            var response = await _serviceContainer.FindAll();
            ViewData["ContainerId"] = new SelectList(response, "Id", "Description");
            return View();
        }

        // POST: Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Quantity,Description,containerId")] Content content)
        public async Task<IActionResult> Create(Content content)
        {
            content.Id = null;
            var resultPost = await client.PostAsync<Content>(requestUri, content, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _serviceContainer.FindAll();
            ViewData["ContainerId"] = new SelectList(response, "Id", "Description");

            var content = await _service.FindOne(id);
            if (content == null)
            {
                return NotFound();
            }


            return View(content);
        }

        // POST: Content/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Description,containerId")] Content content)
        public async Task<IActionResult> Edit(int id, Content content)

        {
            var containerId = content.ContainerId;

            if (id != content.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<Content>(requestUri + content.Id.ToString(), content, new JsonMediaTypeFormatter());
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Details", "Container", new { id = containerId });
        }

        // GET: Content/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var content = await _service.FindOne(id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultDelete = await client.DeleteAsync(requestUri + id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}