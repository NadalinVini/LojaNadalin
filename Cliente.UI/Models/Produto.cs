using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class Produto
    {
        public long Id { get; set; }
        [StringLength(70, MinimumLength = 3)]
        public string Nome { get; set; }        
        public long MarcaId { get; set; }       
        public Marca Marca { get; set; }
    }
}
