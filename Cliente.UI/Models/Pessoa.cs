using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.UI.Models
{
    public class Pessoa
    {
        
        public long Id { get; set; }

        public string Nome { get; set; }
        [StringLength(11, MinimumLength = 11)]
        public string Cpf { get; set; }

        public long? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public int? Numero { get; set; }
        [StringLength(250, MinimumLength = 1)]
        public string Complemento { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
