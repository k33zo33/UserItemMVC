using Items.Dal;
using Items.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Items.Controllers
{
    public class UserController : Controller
    {

        private readonly ICosmosDbService service = CosmosDbServiceProvider.Service!;
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            return View(await service.GetUsersAsync("SELECT * FROM User"));
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(string id) => await ShowUser(id);

        private async Task<ActionResult> ShowUser(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await service.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid().ToString();
                user.Type = nameof(Models.User);
                await service.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id) => await ShowUser(id);

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Type = nameof(Models.User);
                await service.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id) => await ShowUser(id);  

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(User user)
        {
            await service.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
