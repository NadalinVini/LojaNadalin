using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class FormaPagamento
    {
        public int Id { get; set; }

        [StringLength(70, MinimumLength = 3)]
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}