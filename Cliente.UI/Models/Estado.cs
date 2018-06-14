using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class Estado
    {
        public int Id { get; set; }
        [StringLength(250, MinimumLength = 1)]
        public string Nome { get; set; }
    }
}
