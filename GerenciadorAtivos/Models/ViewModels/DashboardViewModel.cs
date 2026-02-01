namespace GerenciadorAtivos.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Apenas contadores e dicionários para gráficos
        public int TotalAtivos { get; set; }
        public int EmUso { get; set; }
        public int Disponiveis { get; set; }
        public int EmManutencao { get; set; }

        public Dictionary<string, int> AtivosPorStatus { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> AtivosPorSetor { get; set; } = new Dictionary<string, int>();
    }
}