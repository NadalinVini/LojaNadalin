using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cliente.UI.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Cliente : IdentityUser
    {     
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
