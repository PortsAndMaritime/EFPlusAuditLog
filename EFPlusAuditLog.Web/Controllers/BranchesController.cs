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
    public class BranchesController : Controller
    {
        private readonly EFPContext _context;

        public BranchesController(EFPContext context)
        {
            _context = context;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            var eFPContext = _context.branches.Include(b => b.Terminal);
            return View(await eFPContext.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.branches
                .Include(b => b.Terminal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Id");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TerminalId")] Branch branch)
        {
            if (ModelState.IsValid)
            {


                var audit = new Audit();
                audit.CreatedBy = "hadi"; // Optional
                _context.Add(branch);
                _context.SaveChanges(audit);

                // _context.Add(branch);
                //  await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Id", branch.TerminalId);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Id", branch.TerminalId);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TerminalId")] Branch branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.Id))
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
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Id", branch.TerminalId);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.branches
                .Include(b => b.Terminal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.branches.FindAsync(id);
            _context.branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.branches.Any(e => e.Id == id);
        }
    }
}
