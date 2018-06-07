namespace Cliente.UI.Models
{
    public class NotaFiscal
    {
        public System.Guid Id { get; set; }
        public System.DateTime DataEmissao { get; set; }

        public int TipoPagamentoId { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
