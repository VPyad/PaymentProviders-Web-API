using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaymentProviders_Web_API.DbContexts;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;

namespace PaymentProviders_Web_API.Controllers
{
    public class PaymentCategoriesController : Controller
    {
        private readonly PaymentProvidersContext _context;

        public PaymentCategoriesController(PaymentProvidersContext context)
        {
            _context = context;
        }

        // GET: PaymentCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: PaymentCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentCategory == null)
            {
                return NotFound();
            }

            return View(paymentCategory);
        }

        // GET: PaymentCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryCode,NameRu")] PaymentCategory paymentCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentCategory);
        }

        // GET: PaymentCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentCategory = await _context.Categories.FindAsync(id);
            if (paymentCategory == null)
            {
                return NotFound();
            }
            return View(paymentCategory);
        }

        // POST: PaymentCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CategoryCode,NameRu")] PaymentCategory paymentCategory)
        {
            if (id != paymentCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentCategoryExists(paymentCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paymentCategory);
        }

        // GET: PaymentCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentCategory == null)
            {
                return NotFound();
            }

            return View(paymentCategory);
        }

        // POST: PaymentCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var paymentCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(paymentCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentCategoryExists(long id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
