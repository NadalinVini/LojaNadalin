using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class Marca
    {
        public long Id { get; set; }
        [StringLength(250, MinimumLength = 1)]
        public string Nome { get; set; }
    }
}