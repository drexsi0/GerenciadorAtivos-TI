using System.ComponentModel.DataAnnotations;

namespace GerenciadorAtivos.Models
{
    public enum SetorAtivo
    {
        [Display(Name = "TI - Desenvolvimento")]
        Desenvolvimento = 1,

        [Display(Name = "TI - Infraestrutura")]
        Infraestrutura = 2,

        [Display(Name = "Recursos Humanos")]
        RH = 3,

        [Display(Name = "Financeiro")]
        Financeiro = 4,

        [Display(Name = "Administrativo")]
        Administrativo = 5,

        [Display(Name = "Operacional")]
        Operacional = 6,

        [Display(Name = "Comercial / Vendas")]
        Comercial = 7,

        [Display(Name = "Marketing")]
        Marketing = 8,

        [Display(Name = "Jurídico")]
        Juridico = 9
    }
}