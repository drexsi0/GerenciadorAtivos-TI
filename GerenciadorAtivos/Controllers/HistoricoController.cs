using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorAtivos.Data;
using X.PagedList;
using Microsoft.AspNetCore.Authorization; // 1. Namespace de Segurança

namespace GerenciadorAtivos.Controllers
{
    [Authorize] // 2. A Tranca: Ninguém entra aqui sem login!
    public class HistoricoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoricoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            // Paginação Manual
            int pageSize = 20;
            int pageIndex = pageNumber ?? 1;

            var query = _context.Historicos
                .Include(h => h.Ativo)
                .OrderByDescending(h => h.DataAcao)
                .AsQueryable();

            var totalItemCount = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var listaPaginada = new StaticPagedList<GerenciadorAtivos.Models.Historico>(items, pageIndex, pageSize, totalItemCount);

            return View(listaPaginada);
        }
    }
}