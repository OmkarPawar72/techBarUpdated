using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Services;
using techBar.Models;

namespace techBar.Controllers
{
    public class SellersController : Controller
    {
        private readonly ISellersService _service;

        public SellersController(ISellersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allSellers = await _service.GetAllAsync();
            return View(allSellers);
        }

        //GET: Sellers/create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Create([Bind("logo,SellerName,Description")] Sellers sellers)
        {
            if (!ModelState.IsValid)
            {
                View(sellers);
            }
            _service.AddAsync(sellers);
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(Index)));
        }


        //****************************************************************************************************************************

        //Get: Sellers/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var sellersDetails = await _service.GetByIdAsync(id);

            if (sellersDetails == null)
            {
                return View("NotFound");
            }
            return View(sellersDetails);
        }

        //Get: Sellers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var sellersDetails = await _service.GetByIdAsync(id);

            if (sellersDetails == null)
            {
                return View("NotFound");
            }
            return View(sellersDetails);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,logo,SellerName,Description")] Sellers sellers)
        {
            if (!ModelState.IsValid)
            {
                View(sellers);
            }
            await _service.UpdateAsync(id, sellers);
            return RedirectToAction(nameof(Index));
        }



        //Get: Product/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var sellersDetails = await _service.GetByIdAsync(id);

            if (sellersDetails == null)
            {
                return View("NotFound");
            }
            return View(sellersDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellersDetails = await _service.GetByIdAsync(id);
            if (sellersDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
