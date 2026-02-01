using GerenciadorAtivos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GerenciadorAtivos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Historico> Historicos { get; set; }
    }
}