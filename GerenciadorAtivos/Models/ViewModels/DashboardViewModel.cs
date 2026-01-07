namespace GerenciadorAtivos.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalAtivos { get; set; }
        public int EmUso { get; set; }
        public int Disponiveis { get; set; }
        public int EmManutencao { get; set; }
    }
}