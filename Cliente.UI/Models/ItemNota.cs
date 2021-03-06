﻿using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    public class ItemNota
    {
        public int Id { get; set; }

        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public System.Guid NotaFiscalId { get; set; }
        public NotaFiscal NotaFiscal { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal ValorUnitario { get; set; }
        public decimal Quantidade { get; set; }
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public decimal PercentualDesconto { get; set; }
    }
}
