using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorAtivos.Data;
using GerenciadorAtivos.Models;
using GerenciadorAtivos.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorAtivos.Controllers
{
    [Authorize]
    public class AtivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AtivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ativos
        // Adicionamos o parâmetro "pageNumber" (pode ser nulo, se for a 1ª vez é null)
        public async Task<IActionResult> Index(string searchString, GerenciadorAtivos.Models.StatusAtivo? statusFilter, int? pageNumber)
        {
            // 1. Mantém os filtros na memória para não perder quando trocar de página
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStatus"] = statusFilter;

            var ativos = from m in _context.Ativos
                         select m;

            // 2. Aplica os filtros (igual fizemos antes)
            if (!String.IsNullOrEmpty(searchString))
            {
                ativos = ativos.Where(s => s.Nome.Contains(searchString) || s.Patrimonio.Contains(searchString));
            }

            if (statusFilter.HasValue)
            {
                ativos = ativos.Where(s => s.Status == statusFilter.Value);
            }

            // 3. Ordenação (Opcional, mas recomendado ordenar por ID ou Nome para a paginação não ficar doida)
            ativos = ativos.OrderByDescending(x => x.Id);

            // 4. Define o tamanho da página (Vamos usar 5 para testar, depois você aumenta para 10 ou 20)
            int pageSize = 5;

            // 5. Retorna a Lista Paginada
            // O "pageNumber ?? 1" significa: se o número da página for nulo, use 1.
            return View(await PaginatedList<GerenciadorAtivos.Models.Ativo>.CreateAsync(ativos.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Ativos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ativo = await _context.Ativos
                // O "Include" é a mágica: traz os dados da tabela relacionada junto!
                .Include(a => a.Historicos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ativo == null) return NotFound();

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
                await _context.SaveChangesAsync(); // Aqui o ativo ganha o ID

                // ADICIONE ESTA LINHA:
                await RegistrarHistorico(ativo.Id, "Criação", "Ativo cadastrado inicialmente no sistema.");

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

                    // ADICIONE ESTA LINHA:
                    await RegistrarHistorico(ativo.Id, "Atualização", $"Dados do ativo atualizados. Status atual: {ativo.Status}");
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
    
    // Método auxiliar para registrar histórico sem repetir código
private async Task RegistrarHistorico(int ativoId, string tipoAcao, string descricao)
        {
            var historico = new Historico
            {
                AtivoId = ativoId,
                TipoAcao = tipoAcao,
                Descricao = descricao,
                DataAcao = DateTime.Now,
                Usuario = User.Identity?.Name ?? "Sistema"

            };

            _context.Historicos.Add(historico);
            await _context.SaveChangesAsync();
        }

    }
}