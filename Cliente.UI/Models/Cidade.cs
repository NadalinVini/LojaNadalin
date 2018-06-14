using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}