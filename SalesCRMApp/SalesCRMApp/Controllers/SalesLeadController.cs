using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCRMApp.Entities;
using SalesCRMApp.Repo;

namespace SalesCRMApp.Controllers
{
    [Authorize]
    public class SalesLeadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesLeadController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.SalesLeads != null ? 
                          View(await _context.SalesLeads.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SalesLeads'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SalesLeads == null)
            {
                return NotFound();
            }

            var salesLead = await _context.SalesLeads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLead == null)
            {
                return NotFound();
            }

            return View(salesLead);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Mobile,Email,Source")] SalesLead salesLead)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesLead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesLead);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SalesLeads == null)
            {
                return NotFound();
            }

            var salesLead = await _context.SalesLeads.FindAsync(id);
            if (salesLead == null)
            {
                return NotFound();
            }
            return View(salesLead);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Mobile,Email,Source")] SalesLead salesLead)
        {
            if (id != salesLead.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesLead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesLeadExists(salesLead.Id))
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
            return View(salesLead);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SalesLeads == null)
            {
                return NotFound();
            }

            var salesLead = await _context.SalesLeads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLead == null)
            {
                return NotFound();
            }

            return View(salesLead);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SalesLeads == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SalesLeads'  is null.");
            }
            var salesLead = await _context.SalesLeads.FindAsync(id);
            if (salesLead != null)
            {
                _context.SalesLeads.Remove(salesLead);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesLeadExists(int id)
        {
          return (_context.SalesLeads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
