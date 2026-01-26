using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorAtivos.Data;
using GerenciadorAtivos.Models;

namespace GerenciadorAtivos.Controllers
{
    public class AtivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AtivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ativos
        public async Task<IActionResult> Index(string searchString, GerenciadorAtivos.Models.StatusAtivo? statusFilter)
        {
            // 1. Guarda os filtros para a View poder "lembrar" deles
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStatus"] = statusFilter;

            // 2. Query Base
            var ativos = from m in _context.Ativos
                         select m;

            // 3. Aplica Filtro de Texto (se houver)
            if (!String.IsNullOrEmpty(searchString))
            {
                ativos = ativos.Where(s => s.Nome.Contains(searchString)
                                        || s.Patrimonio.Contains(searchString));
            }

            // 4. Aplica Filtro de Status (se houver)
            // O ".HasValue" serve para verificar se o usuário selecionou algo no dropdown (não nulo)
            if (statusFilter.HasValue)
            {
                ativos = ativos.Where(s => s.Status == statusFilter.Value);
            }

            // 5. Executa e retorna
            return View(await ativos.ToListAsync());
        }

        // GET: Ativos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ativo = await _context.Ativos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ativo == null)
            {
                return NotFound();
            }

            return View(ativo);
        }

        // GET: Ativos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ativos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Patrimonio,Tipo,Marca,Modelo,Setor,Status")] Ativo ativo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ativo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ativo);
        }

        // GET: Ativos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ativo = await _context.Ativos.FindAsync(id);
            if (ativo == null)
            {
                return NotFound();
            }
            return View(ativo);
        }

        // POST: Ativos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Patrimonio,Tipo,Marca,Modelo,Setor,Status")] Ativo ativo)
        {
            if (id != ativo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ativo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtivoExists(ativo.Id))
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
            return View(ativo);
        }

        // GET: Ativos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ativo = await _context.Ativos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ativo == null)
            {
                return NotFound();
            }

            return View(ativo);
        }

        // POST: Ativos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ativo = await _context.Ativos.FindAsync(id);
            if (ativo != null)
            {
                _context.Ativos.Remove(ativo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtivoExists(int id)
        {
            return _context.Ativos.Any(e => e.Id == id);
        }
    }
}
