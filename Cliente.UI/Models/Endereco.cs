namespace Cliente.UI.Models
{
    public class Endereco
    {
        public long Id { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
        public string Cep { get; set; }
    }
}