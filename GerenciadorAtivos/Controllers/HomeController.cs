using GerenciadorAtivos.Data; // Importante para ver o banco
using GerenciadorAtivos.Models; // Importante para ver os Enums
using GerenciadorAtivos.Models.ViewModels; // Importante para ver a ViewModel
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GerenciadorAtivos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // 1. Campo para guardar o banco

        // 2. Injeção de Dependência no Construtor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; // O sistema entrega o banco pronto aqui
        }

        public IActionResult Index()
        {
            var ativos = _context.Ativos.ToList();

            var viewModel = new DashboardViewModel
            {
                TotalAtivos = ativos.Count,
                EmUso = ativos.Count(x => x.Status == StatusAtivo.EmUso),
                Disponiveis = ativos.Count(x => x.Status == StatusAtivo.Disponivel),
                EmManutencao = ativos.Count(x => x.Status == StatusAtivo.Manutencao),

                // Removida a lógica de ValorTotalPatrimonio

                AtivosPorStatus = ativos
                    .GroupBy(x => x.Status)
                    .ToDictionary(
                        g => g.Key?.ToString() ?? string.Empty, // Ensure non-null string key
                        g => g.Count()),

                AtivosPorSetor = ativos
    .GroupBy(x => x.Setor) // x.Setor aqui é um SetorAtivo (Enum)
    .ToDictionary(
        g => g.Key.ToString(), // Chave = "Desenvolvimento" (Nome do Enum)
        g => g.Count()
    )
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}