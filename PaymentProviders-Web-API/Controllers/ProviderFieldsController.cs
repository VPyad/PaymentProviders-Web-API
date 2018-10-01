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
    public class ProviderFieldsController : Controller
    {
        private readonly PaymentProvidersContext _context;

        public ProviderFieldsController(PaymentProvidersContext context)
        {
            _context = context;
        }

        // GET: ProviderFields
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProviderFields.ToListAsync());
        }

        // GET: ProviderFields/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerField = await _context.ProviderFields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (providerField == null)
            {
                return NotFound();
            }

            return View(providerField);
        }

        // GET: ProviderFields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProviderFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,InterfaceType,Name,Required,Direction,DontShow,Mask,Title,Comment,MinLength,MaxLength,RegExp,DontTicket")] ProviderField providerField)
        {
            if (ModelState.IsValid)
            {
                _context.Add(providerField);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(providerField);
        }

        // GET: ProviderFields/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerField = await _context.ProviderFields.FindAsync(id);
            if (providerField == null)
            {
                return NotFound();
            }
            return View(providerField);
        }

        // POST: ProviderFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,InterfaceType,Name,Required,Direction,DontShow,Mask,Title,Comment,MinLength,MaxLength,RegExp,DontTicket")] ProviderField providerField)
        {
            if (id != providerField.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(providerField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderFieldExists(providerField.Id))
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
            return View(providerField);
        }

        // GET: ProviderFields/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerField = await _context.ProviderFields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (providerField == null)
            {
                return NotFound();
            }

            return View(providerField);
        }

        // POST: ProviderFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var providerField = await _context.ProviderFields.FindAsync(id);
            _context.ProviderFields.Remove(providerField);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderFieldExists(long id)
        {
            return _context.ProviderFields.Any(e => e.Id == id);
        }
    }
}
