using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudVendasMvc.Models;

namespace CrudVendasMvc.Controllers
{
    public class VendasRegistrosController : Controller
    {
        private readonly CrudVendasMvcContext _context;

        public VendasRegistrosController(CrudVendasMvcContext context)
        {
            _context = context;
        }

        // GET: VendasRegistros
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendasRegistro.ToListAsync());
        }

        // GET: VendasRegistros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendasRegistro = await _context.VendasRegistro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendasRegistro == null)
            {
                return NotFound();
            }

            return View(vendasRegistro);
        }

        // GET: VendasRegistros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendasRegistros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Quantia,Estado")] VendasRegistro vendasRegistro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendasRegistro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendasRegistro);
        }

        // GET: VendasRegistros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendasRegistro = await _context.VendasRegistro.FindAsync(id);
            if (vendasRegistro == null)
            {
                return NotFound();
            }
            return View(vendasRegistro);
        }

        // POST: VendasRegistros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Quantia,Estado")] VendasRegistro vendasRegistro)
        {
            if (id != vendasRegistro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendasRegistro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendasRegistroExists(vendasRegistro.Id))
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
            return View(vendasRegistro);
        }

        // GET: VendasRegistros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendasRegistro = await _context.VendasRegistro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendasRegistro == null)
            {
                return NotFound();
            }

            return View(vendasRegistro);
        }

        // POST: VendasRegistros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendasRegistro = await _context.VendasRegistro.FindAsync(id);
            _context.VendasRegistro.Remove(vendasRegistro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendasRegistroExists(int id)
        {
            return _context.VendasRegistro.Any(e => e.Id == id);
        }
    }
}
