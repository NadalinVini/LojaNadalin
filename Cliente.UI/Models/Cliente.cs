using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Cliente.UI.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Cliente : IdentityUser
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public long? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
    }
}
