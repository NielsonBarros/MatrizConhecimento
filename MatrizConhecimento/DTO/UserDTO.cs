using MatrizConhecimento.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatrizConhecimento.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O nome de usuário é requerido.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é requerida.")]
        [DisplayName("Nascimento")]
        public DateTime? BirthDay { get; set; }

        [Required(ErrorMessage = "O CPF é requerido.")]
        [DisplayName("CPF")]
        [CPFAttribute(ErrorMessage ="CPF Inválido")]
        public string CPF { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        [Required(ErrorMessage = "A senha é necessário.")]
        [MinLength(6, ErrorMessage = "A senha precisar ter 6 caracteres")]
        public string Password { get; set; }

        [DisplayName("Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As Senhas não são iguais.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
