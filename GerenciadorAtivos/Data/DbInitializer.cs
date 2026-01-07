using GerenciadorAtivos.Models;

namespace GerenciadorAtivos.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Garante que o banco foi criado
            context.Database.EnsureCreated();

            // Verifica se já existem ativos. Se tiver, não faz nada.
            if (context.Ativos.Any())
            {
                return;   // O banco já tem dados
            }

            // Se chegou aqui, é porque está vazio. Vamos criar os dados!
            var ativos = new Ativo[]
            {
                new Ativo { Nome = "Dell Latitude 5420", Patrimonio = "NT-001", Tipo = TipoAtivo.Notebook, Marca = "Dell", Modelo = "Latitude 5420", Setor = "TI - Desenvolvimento", Status = StatusAtivo.EmUso },
                new Ativo { Nome = "Monitor LG Ultrawide", Patrimonio = "MN-055", Tipo = TipoAtivo.Monitor, Marca = "LG", Modelo = "29WK600", Setor = "TI - Design", Status = StatusAtivo.Disponivel },
                new Ativo { Nome = "MacBook Pro M3", Patrimonio = "NT-002", Tipo = TipoAtivo.Notebook, Marca = "Apple", Modelo = "Pro 14", Setor = "Diretoria", Status = StatusAtivo.EmUso },
                new Ativo { Nome = "Teclado Mecânico Logitech", Patrimonio = "PE-201", Tipo = TipoAtivo.Periferico, Marca = "Logitech", Modelo = "MX Keys", Setor = "RH", Status = StatusAtivo.Manutencao },
                new Ativo { Nome = "Servidor Dell PowerEdge", Patrimonio = "SRV-01", Tipo = TipoAtivo.Servidor, Marca = "Dell", Modelo = "R750", Setor = "Data Center", Status = StatusAtivo.EmUso }
            };

            // Adiciona o array acima no banco
            context.Ativos.AddRange(ativos);

            // Salva as alterações
            context.SaveChanges();
        }
    }
}