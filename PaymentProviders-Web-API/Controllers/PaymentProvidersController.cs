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
    public class PaymentProvidersController : Controller
    {
        private readonly PaymentProvidersContext _context;

        public PaymentProvidersController(PaymentProvidersContext context)
        {
            _context = context;
        }

        // GET: PaymentProviders
        public async Task<IActionResult> Index()
        {
            var paymentProvidersContext = _context.PaymentProviders.Include(p => p.Category);
            return View(await paymentProvidersContext.ToListAsync());
        }

        // GET: PaymentProviders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentProvider = await _context.PaymentProviders
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentProvider == null)
            {
                return NotFound();
            }

            return View(paymentProvider);
        }

        // GET: PaymentProviders/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: PaymentProviders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProviderCode,NameRu,PaymentInfoId,CatalogCode,CategoryId,Deleted,Mrlist,MultiCheck,NoSavePt,Check,IsSupportRequestRSTEP,Order,ChequeName,RegionString")] PaymentProvider paymentProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", paymentProvider.CategoryId);
            return View(paymentProvider);
        }

        // GET: PaymentProviders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentProvider = await _context.PaymentProviders.FindAsync(id);
            if (paymentProvider == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", paymentProvider.CategoryId);
            return View(paymentProvider);
        }

        // POST: PaymentProviders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProviderCode,NameRu,PaymentInfoId,CatalogCode,CategoryId,Deleted,Mrlist,MultiCheck,NoSavePt,Check,IsSupportRequestRSTEP,Order,ChequeName,RegionString")] PaymentProvider paymentProvider)
        {
            if (id != paymentProvider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentProviderExists(paymentProvider.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", paymentProvider.CategoryId);
            return View(paymentProvider);
        }

        // GET: PaymentProviders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentProvider = await _context.PaymentProviders
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentProvider == null)
            {
                return NotFound();
            }

            return View(paymentProvider);
        }

        // POST: PaymentProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var paymentProvider = await _context.PaymentProviders.FindAsync(id);
            _context.PaymentProviders.Remove(paymentProvider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentProviderExists(long id)
        {
            return _context.PaymentProviders.Any(e => e.Id == id);
        }
    }
}
