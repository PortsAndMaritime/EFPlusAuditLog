using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFPlusAuditLog.Web.Models;
using Z.EntityFramework.Plus;

namespace EFPlusAuditLog.Web.Controllers
{
    public class TerminalsController : Controller
    {
        private readonly EFPContext _context;



        public TerminalsController(EFPContext context)
        {
            _context = context;
        }

        // GET: Terminals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Terminal.ToListAsync());
        }

        // GET: Terminals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        // GET: Terminals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Terminals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Location")] Terminal terminal)
        {
            if (ModelState.IsValid)
            {

                var audit = new Audit();
                audit.CreatedBy = "hadi"; // Optional
                _context.Add(terminal);
                _context.SaveChanges(audit);



                
             //   await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(terminal);
        }

        // GET: Terminals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal.FindAsync(id);
            if (terminal == null)
            {
                return NotFound();
            }
            return View(terminal);
        }

        // POST: Terminals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Location")] Terminal terminal)
        {
            if (id != terminal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terminal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminalExists(terminal.Id))
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
            return View(terminal);
        }

        // GET: Terminals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terminal = await _context.Terminal.FindAsync(id);
            _context.Terminal.Remove(terminal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminalExists(int id)
        {
            return _context.Terminal.Any(e => e.Id == id);
        }
    }
}
