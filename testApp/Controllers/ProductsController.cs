using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testApp.Models;
using testApp.Services;

namespace testApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _service = new ProductService();

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_service.ProductExists(id))
                return View(_service.GetById(id));
            else
                return RedirectToAction("Index");
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _service.Create(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_service.ProductExists(id))
                return View(_service.GetById(id));
            else
                return RedirectToAction("Index");
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Stock")] Product product)
        {
            _service.Edit(id, product);
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if(_service.ProductExists(id))
                return View(_service.GetById(id));
            else
                return RedirectToAction("Index");
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
