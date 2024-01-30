using Items.Dal;
using Items.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Items.Controllers
{
    public class ItemController : Controller
    {
        private const string SelectAllUsersQuery = "SELECT * FROM User";

        private readonly ICosmosDbService service = CosmosDbServiceProvider.Service!;

        // GET: ItemController
        public async Task<ActionResult> Index()
        {

            var items = await service.GetItemsAsync("SELECT * FROM Item");

            foreach (var item in items)
            {
                var user = await service.GetUserAsync(item.UserId);
                ViewBag.UserFullName = user?.FullName;
            }
            return View(items);
        }

        // GET: ItemController/Details/5
        public async Task<ActionResult> Details(string id) => await ShowItem(id);

        private async Task<ActionResult> ShowItem(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await service.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        private async Task<IEnumerable<Models.User>> GetUsersForItemsAsync()
        {
            return await service.GetUsersAsync(SelectAllUsersQuery);
        }

        // GET: ItemController/Create
        public ActionResult Create()
        {
            ViewBag.Users = GetUsersForItemsAsync().Result;
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item item)
        {

            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                item.Type = nameof(Item);
                await service.AddItemAsync(item);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = GetUsersForItemsAsync().Result;
            return View(item);
        }

        // GET: ItemController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            ViewBag.Users = GetUsersForItemsAsync().Result;
            return View();
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Type = nameof(Item);
                await service.UpdateItemAsync(item);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = GetUsersForItemsAsync().Result;
            return View(item);
        }

        // GET: ItemController/Delete/5
        public async Task<ActionResult> Delete(string id) => await ShowItem(id);

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Item item)
        {
            await service.DeleteItemAsync(item);
            return RedirectToAction(nameof(Index));
        }
    }
}
