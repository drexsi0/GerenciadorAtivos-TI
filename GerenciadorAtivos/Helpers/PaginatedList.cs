using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAtivos.Helpers
{
    // O <T> significa que essa paginação funciona para Ativos, Usuários, Log, QUALQUER COISA.
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            // Calcula quantas páginas total (Ex: 100 itens / 10 por pag = 10 páginas)
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        // Propriedade inteligente: Tem página anterior?
        public bool HasPreviousPage => PageIndex > 1;

        // Propriedade inteligente: Tem próxima página?
        public bool HasNextPage => PageIndex < TotalPages;

        // Método estático para criar a página de forma assíncrona (Performance pura)
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync(); // Conta no banco quanto tem no total
            var items = await source.Skip((pageIndex - 1) * pageSize) // Pula os anteriores
                                    .Take(pageSize) // Pega só os 10 da vez
                                    .ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}