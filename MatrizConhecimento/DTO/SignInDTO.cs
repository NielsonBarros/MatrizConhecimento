using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatrizConhecimento.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public virtual string UserName { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }
}
