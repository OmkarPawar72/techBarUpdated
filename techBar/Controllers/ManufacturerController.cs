using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Services;
using techBar.Data.Static;
using techBar.Models;

namespace techBar.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _service;

        public ManufacturerController(IManufacturerService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allManufacturer = await _service.GetAllAsync();
            return View(allManufacturer);
        }

        //GET: manufacturerDetails/details/1
        
        public async Task<IActionResult> Details(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
            {
                return View("NotFound");
            }
            return View(manufacturerDetails);
        }

        //GET: Manufacturer/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public Task<IActionResult> Create([Bind("ProfilePitctureURL,FullName,Bio")] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                View(manufacturer);
            }
            _service.AddAsync(manufacturer);
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(Index)));
        }

        //GET: Manufacturer/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
            {
                return View("NotFound");
            }
            return View(manufacturerDetails);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePitctureURL,FullName,Bio")] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                View(manufacturer);
            }
            if (id == manufacturer.Id)
            {
                await _service.UpdateAsync(id, manufacturer);
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        //Get: Product/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);

            if (manufacturerDetails == null)
            {
                return View("NotFound");
            }
            return View(manufacturerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
