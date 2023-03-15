using Microsoft.AspNetCore.Mvc;
using techBar.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using techBar.Data.Services;
using techBar.Models;

namespace techBar.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Create([Bind("Id,ProfilePitctureURL,FullName,Bio")] Product product)
        {
            if (ModelState.IsValid)
            {
                View(product);
            }
            _service.AddAsync(product);
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(Index)));
        }

        //Get: Product/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null)
            {
                return View("NotFound");
            }
            return View(productDetails);
        }

        //Get: Product/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null)
            {
                return View("NotFound");
            }
            return View(productDetails);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePitctureURL,FullName,Bio")] Product product)
        {
            if (!ModelState.IsValid)
            {
                View(product);
            }
            await _service.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }



        //Get: Product/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null)
            {
                return View("NotFound");
            }
            return View(productDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
