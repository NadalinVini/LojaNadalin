using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class NotaFiscal
    {
        public System.Guid Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public System.DateTime DataEmissao { get; set; }

        public int TipoPagamentoId { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public long PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
