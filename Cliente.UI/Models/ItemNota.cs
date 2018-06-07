namespace Cliente.UI.Models
{
    public class ItemNota
    {
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public System.Guid NotaFiscalId { get; set; }
        public NotaFiscal NotaFiscal { get; set; }

        public decimal ValorUnitario { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PercentualDesconto { get; set; }
    }
}
