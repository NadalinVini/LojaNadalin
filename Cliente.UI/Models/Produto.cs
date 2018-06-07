namespace Cliente.UI.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public long MarcaId { get; set; }
        public Marca Marca { get; set; }
    }
}
