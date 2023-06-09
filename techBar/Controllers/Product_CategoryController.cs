﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Services;
using techBar.Data.Static;
using techBar.Models;

namespace techBar.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsCategoryController : Controller
    {
        private readonly IProductsCategoryService _service;

        public ProductsCategoryController(IProductsCategoryService productsCategoryService)
        {
            _service = productsCategoryService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n=>n.Sellers);
            return View(allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync(n => n.Sellers);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = allProducts.Where(n=>n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index",filterResult);
            }

            return View("Index",allProducts);
        }

        //GET: ProductsCategory/Details/1
        [AllowAnonymous]
        public async Task<IActionResult>Details(int id)
        {
            var productDetails = await _service.GetCategoryIdAsync(id);
            return View(productDetails);
        }

        //GET : ProductsCategory/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
            ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
            ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductsCategoryVM productsCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownData = await _service.GetNewProductsDropdownsValues();

                ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
                ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
                ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

                return View(productsCategoryVM);
            }
            await _service.AddNewProductCategoryAsync(productsCategoryVM);
            return RedirectToAction(nameof(Index));
        }

        //GET : ProductsCategory/Edit/1
        //[HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var details = await _service.GetCategoryIdAsync(id);
            if (details == null) 
            {
                return View("NotFound");
            };

            var response = new NewProductsCategoryVM()
            {
                Id = details.Id,  
                Name = details.Name,
                Description = details.Description,
                Price = details.Price,
                StartDate= details.StartDate,
                EndDate= details.EndDate,
                ImageURL = details.ImageURL,
                ProductCategory = details.ProductCategory,
                SellerId = details.SellerId,
                ManufacturerId = details.ManufacturerId,
                ProductsIds = details.Products_Categories.Select(n => n.ProductId).ToList()
            };


            var productDropdownData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
            ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
            ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

            return View(response);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductsCategoryVM productsCategoryVM)
        {
            if (id != productsCategoryVM.Id)
            {
                View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var productDropdownData = await _service.GetNewProductsDropdownsValues();

                ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
                ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
                ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

                return View(productsCategoryVM);
            }
            //Error Causing here.
            await _service.UpdateProductAsync(productsCategoryVM);
            return RedirectToAction(nameof(Index));
        }
    }
}
