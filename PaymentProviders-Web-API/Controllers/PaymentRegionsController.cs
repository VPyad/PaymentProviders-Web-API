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
    public class PaymentRegionsController : Controller
    {
        private readonly PaymentProvidersContext _context;

        public PaymentRegionsController(PaymentProvidersContext context)
        {
            _context = context;
        }

        // GET: PaymentRegions
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentRegions.ToListAsync());
        }

        // GET: PaymentRegions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentRegion = await _context.PaymentRegions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentRegion == null)
            {
                return NotFound();
            }

            return View(paymentRegion);
        }

        // GET: PaymentRegions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentRegions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name")] PaymentRegion paymentRegion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentRegion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentRegion);
        }

        // GET: PaymentRegions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentRegion = await _context.PaymentRegions.FindAsync(id);
            if (paymentRegion == null)
            {
                return NotFound();
            }
            return View(paymentRegion);
        }

        // POST: PaymentRegions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name")] PaymentRegion paymentRegion)
        {
            if (id != paymentRegion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentRegion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentRegionExists(paymentRegion.Id))
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
            return View(paymentRegion);
        }

        // GET: PaymentRegions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentRegion = await _context.PaymentRegions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentRegion == null)
            {
                return NotFound();
            }

            return View(paymentRegion);
        }

        // POST: PaymentRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var paymentRegion = await _context.PaymentRegions.FindAsync(id);
            _context.PaymentRegions.Remove(paymentRegion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentRegionExists(long id)
        {
            return _context.PaymentRegions.Any(e => e.Id == id);
        }
    }
}
